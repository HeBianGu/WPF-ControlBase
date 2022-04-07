// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Control.Diagram;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HeBianGu.Systems.Component
{
    public class ComponentService
    {
        /// <summary>
        /// 获取所有目录下面的组件
        /// </summary>
        /// <returns></returns>
        public static List<IComponentNode> GetComponents()
        {
            List<IComponentNode> result = new List<IComponentNode>();

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Component");

            if (!Directory.Exists(path)) return null;

            string[] dlls = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);

            List<Assembly> assemblys = dlls.Select(l => Assembly.LoadFrom(l)).Where(l => l.GetCustomAttribute<ComponentAttribute>() != null)?.ToList();

            List<Type> types = assemblys.SelectMany(l => l.GetTypes()).Where(l => typeof(IComponentNode).IsAssignableFrom(l)).ToList();

            if (types == null) return null;

            return types.Select(l => Activator.CreateInstance(l)).Cast<IComponentNode>().ToList();
        }

        /// <summary>
        /// 筛选所有组件
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static List<IComponentNode> GetComponents(Predicate<ComponentAttribute> predicate)
        {
            return ComponentService.GetComponents().Where(k => predicate.Invoke(k.GetType().Assembly.GetCustomAttribute<ComponentAttribute>()))?.ToList();
        }

        /// <summary>
        /// 获取所有目录下面的组件
        /// </summary>
        /// <returns></returns>
        public static List<Assembly> GetAssemblys()
        {
            List<IComponentNode> result = new List<IComponentNode>();

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Component");

            if (!Directory.Exists(path)) return null;

            string[] dlls = Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly);

            return dlls.Select(l => Assembly.LoadFrom(l)).Where(l => l.GetCustomAttribute<ComponentAttribute>() != null)?.ToList();
        }


        public static async Task<bool> StartOnlyNode(Node startNode)
        {
            Func<Node, Task<bool>> run = null;

            run = async l =>
            {
                List<Node> tos = l.GetToNodes();

                foreach (Node node in tos)
                {
                    if (node.Content is IAction action)
                    {
                        IActionResult result = await action.TryInvokeAsync(l.Content as IAction);

                        if (result.State == ResultState.Error)
                        {
                            return false;
                        }

                        bool b = await run?.Invoke(node);

                        if (!b) return b;
                    }
                }

                return true;
            };

            return await run?.Invoke(startNode);
        }

        public static async Task<bool> StartAllAction(Node startNode)
        {
            Func<Node, Task<bool>> run = null;

            run = async l =>
            {
                if (l.Content is IComponentNode nodeData)
                {
                    //  ToDo：需要传递上一个IAction
                    INodeActionResult result = await nodeData.TryInvokeAsync(null) as INodeActionResult;

                    if (result == null || result.State == ResultState.Error)
                    {
                        return false;
                    }

                    //  Do ：遍历输出节点
                    if (result.Ports == null || result.Ports.Count == 0) return false;

                    foreach (Link link in l.LinksOutOf)
                    {
                        if (link.FromPort?.Content is IComponentPort portData)
                        {
                            IComponentPort find = result.Ports.Find(k => k == portData);

                            if (find == null) continue;

                            if (link.Content is IAction linkData)
                            {
                                //  Do ：执行FromPort
                                await portData.TryInvokeAsync(nodeData);

                                //  Do ：执行link
                                await linkData.TryInvokeAsync(portData);

                                //  Do ：执行ToPort
                                if (link.ToPort?.Content is IComponentPort toPort)
                                {
                                    await toPort.TryInvokeAsync(linkData);
                                }

                                //  Do ：递归执行ToNode
                                bool b = await run?.Invoke(link.ToNode);

                                if (!b) return b;
                            }
                        }
                    }

                }

                return true;
            };

            return await run.Invoke(startNode);
        }

        public static IEnumerable<ComponentMetadata> GetOnlineComponents()
        {
            ComponentMetadata result = new ComponentMetadata();

            string[] files = Directory.GetFiles(@"D:\GitHub\WPF-Workflow\Product\Debug");

            foreach (string item in files)
            {
                if (Path.GetExtension(item).EndsWith("dll"))
                {
                    ComponentMetadata metadata = new ComponentMetadata();

                    metadata.Version = Assembly.LoadFrom(item).GetName().Version.ToString();
                    metadata.Name = Path.GetFileName(item);
                    metadata.Description = Path.GetFileName(item);
                    metadata.Tag = Path.GetFileName(item);
                    metadata.Author = "HeBianGu";

                    yield return metadata;
                }
            }
        }

        public static IEnumerable<ComponentMetadata> GetLocalComponents()
        {
            List<Assembly> components = GetAssemblys();

            foreach (Assembly item in components)
            {
                ComponentMetadata metadata = new ComponentMetadata();

                metadata.Version = item.GetName().Version.ToString();
                metadata.Name = item.GetName().Name;
                metadata.Description = item.GetName().Name;
                metadata.Tag = item.GetName().Name;
                metadata.Author = "HeBianGu";

                yield return metadata;
            }
        }
    }

    public class ComponentMetadata
    {
        [Display(Name = "名称")]
        public string Name { get; set; }

        [Browsable(false)]
        public int Order { get; set; }

        [Browsable(false)]
        public string Icon { get; set; }

        [Display(Name = "作者")]

        public string Author { get; set; }
        [Display(Name = "描述")]

        public string Description { get; set; }
        [Display(Name = "标签")]

        public string Tag { get; set; }
        [Display(Name = "URL")]

        public string Url { get; set; }
        [Display(Name = "版本")]

        public string Version { get; set; }
    }
}

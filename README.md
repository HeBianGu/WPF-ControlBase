

# <img align="left" src="./Document/Resource/logo.jpg" width="40"/> WPF-Controls  |  [English](README.md)

<p align="left"> 
    <img alt="dotnet-version" src="https://img.shields.io/badge/.net-v4.5-blue.svg"></img>
    <img alt="dotnet-version" src="https://img.shields.io/badge/.net-v4.5.1-blue.svg"></img>
    <img alt="dotnet-version" src="https://img.shields.io/badge/.net-v4.5.2-blue.svg"></img>
    <img alt="dotnet-version" src="https://img.shields.io/badge/.net-v4.6-blue.svg"></img>
    <img alt="dotnet-version" src="https://img.shields.io/badge/.net-v4.6.1-blue.svg"></img>
    <img alt="dotnet-version" src="https://img.shields.io/badge/.net-v4.6.2-blue.svg"></img>
    <img alt="dotnet-version" src="https://img.shields.io/badge/.net-v4.7-blue.svg"></img>
    <img alt="dotnet-version" src="https://img.shields.io/badge/.net-v4.7.1-blue.svg"></img>
    <img alt="dotnet-version" src="https://img.shields.io/badge/.net-v4.7.2-blue.svg"></img>
    <img alt="dotnet-version" src="https://img.shields.io/badge/.net-v4.8-blue.svg"></img>
    <img alt="dotnet-version" src="https://img.shields.io/badge/.net-v4.8.1-blue.svg"></img>
    <img alt="dotnet-version" src="https://img.shields.io/badge/.net-v4.8.2-blue.svg"></img>
    <img alt="dotnet-version" src="https://img.shields.io/badge/.netcore-v3.1;-blue.svg"></img>
    <img alt="dotnet-version" src="https://img.shields.io/badge/.net-v5.0-windows.svg"></img>
</p>

<p align="left"> 
    <img alt="csharp-version" src="https://img.shields.io/badge/C%23-7.0-blue.svg"></img>
    <img alt="IDE-version" src="https://img.shields.io/badge/IDE-vs2022-blue.svg"></img>
</p>

<p align="left"> 
    <a href="https://www.nuget.org/packages?q=hebiangu">
        <img alt="nuget-version" src="https://img.shields.io/nuget/v/HeBianGu.General.WpfControlLib.svg"></img>
    </a>
     <a href="https://github.com/HeBianGu/WPF-Controls/actions?query=workflow%3Abuild">
        <img alt="Github-build-status" src="https://github.com/HeBianGu/WPF-Controls/actions/workflows/main.yml/badge.svg"></img>
    </a>
</p>

 ## 关于
 ### Star

[![Stargazers over time](https://starchart.cc/HeBianGu/WPF-ControlBase.svg)](https://starchart.cc/HeBianGu/WPF-ControlBase)
 
## 预览
 

## 案例
### Creator
### Map
### Chart
### Office
![qrcode](https://gitee.com/hebiangu/wpf-document/raw/master/WPF-Controls/office.gif)
### Disk
![qrcode](https://gitee.com/hebiangu/wpf-document/raw/master/WPF-Controls/disk.gif)
### Scene
![qrcode](https://gitee.com/hebiangu/wpf-document/raw/master/WPF-Controls/github.gif)
### Manager
### Menu
### Above
### Blur
### Blur
### Tool
### Touch
### Repository
### Main

## 基础控件
### Button
### TextBox
### CheckBox
### RadioButton
### ToggleButton
### ComboBox
### DatePicker
### Slider
### ProgressBar
### Expander
### ListBox
### DataGrid
### TreeView
### TabControl
### ContextMenu
### Menu
### Label
### TextBlock
### ToolTip

## 自定义控件
### PropertyGrid
### Chart2D
### Diagram
### Drawer
### Explorer
### ExplorerTree
### FilterBox
### FilterColumn
### SearchBox
### SelectionBox
### GridSplitter
### Guide
### ImagePlayer
### AnimationBox
### AnimatedTabControl
### LeftMenu
### Host
### Message
### MessageContainer
### MessageListBox
### MultiComboBox
### PagedDataGrid
### Panel
### PasswordBox
### Ping
### ScrollInto
### ScrollVewerLocator
### SearchComboBox
### Shape
### Shuttle
### StoryBoard
### TextEditor
### ThemeSet
### ToggleExpander
### TopContainer
### TransformAdorner
### TreeListView
### Vlc

## 自定义窗口
### Blur
### Float
### Link
### Login
### Main
### Menu
### MessageDialog
### Notify
### Ribbon
### Start

## 特性
### Identity
### Setting
### Repository
### Upgrade
### XmlSerialize
### Project
### Operation
### License
### Component
### Automation
### Module
### Mvc
### Mvp
### Image
### Validation
### ViewModel
### Animation
### MarkupExtension
### AppConfig
### Command
### Converter
### Interactivity
### TypeConverter

## 使用

### 示例
#### 这是一个最简单的使用方式示例
HeBianGu.Demo.Demo1
#### 这是一个基础控件中的ResourceKey示例
HeBianGu.Demo.Demo2
```C#  
        <Button Style="{DynamicResource {x:Static h:ButtonKeys.Accent}}" />
```
#### 这是一个公用基础的ResourceKey示例
HeBianGu.Demo.Demo3
```XAML 
        <Button Background="{DynamicResource {x:Static h:BrushKeys.BackgroundDefault}}"/>
```
#### 这是一个最简单的MainWindow窗口示例
HeBianGu.Demo.Demo4
```C#  
   //  Do ：继承ApplicationBase
   public partial class App : ApplicationBase
    {
       
    }
    //  Do ：继承h:MainWindow
    <h:MainWindow x:Class="HeBianGu.Demo.Demo4.MainWindow"
```
#### 这是一个注册主题设置和注册保存配置信息的示例
HeBianGu.Demo.Demo5
```C#  
            //  Do ：注册窗口配置，注册后窗口右侧有可设置主题的按钮
            services.AddTheme();

            //  Do ：注册序列化保存接口，注册后主题的配置会保存到本地，再次启动会读取
            services.AddXmlSerialize();
            
            //  Do：设置默认主题
            app.UseLocalTheme(l =>
            {
                l.AccentColor = (Color)ColorConverter.ConvertFromString("#FF0093FF");
                l.SmallFontSize = 14D;
                l.LargeFontSize = 16D;
                l.FontSize = FontSize.Small;
                l.ItemHeight = 36;
                l.RowHeight = 40;
                l.ItemCornerRadius = 5;
                l.AnimalSpeed = 5000;
                l.AccentColorSelectType = 0;
                l.IsUseAnimal = false;
                l.ThemeType = ThemeType.Light;
                l.Language = Language.Chinese;
                l.AccentBrushType = AccentBrushType.LinearGradientBrush;
            });
```
#### 这是一个注册框架对话框的示例
HeBianGu.Demo.Demo6
```C#  
            //  Do ：注册后可以使用框架自带的对话框
            services.AddMessageDialog();
```
#### 这是一个注册框架配置页面的示例
HeBianGu.Demo.Demo7
```C#  
            //  Do ：注册配置加载方式
            services.AddSetting();

            //  Do ：注册右上角配置页面
            services.AddSettingViewPrenter();
            
            //  Do ：添加自定义配置信息
            app.UseSetting(l =>
            {
                l.Settings.Add(TestSetting.Instance);
            });
```
#### 这是一个注册启动页面的示例
HeBianGu.Demo.Demo8
```C#  
            //  Do ：注册启动页面
            services.AddStart();
            
            //  Do ：添加启动窗口配置
            app.UseStart(l =>
            {
                l.Title = "HeBianGu";
                l.TitleFontSize = 80;
            });
```
#### 这是一个注册登录页面的示例
HeBianGu.Demo.Demo9
```C#  
            //  Do ：注册登录页面和使用测试接口
            services.AddIdentity();
            
            //  Do ：添加身份认证配置
            app.UseIdentity(l =>
            {

            });
```
#### 这是一个注册自动更新页面的示例
HeBianGu.Demo.Demo10
```C#  
            //  Do ：注册软件更新页面
            services.AddUpgrade();
            
            //  Do ：添加软件更新配置
            app.UseUpgrade(l =>
            {

            });
```
其他示例待更新...
#### 注
目前大部分功能采用注入(Add)和配置(Use)的方式添加.
系统提供默认方法，如：services.AddMessageDialog()，如果不想使用系统默认对话框则替换成注册方法，重新注册接口即可，如:service.AddSingleton<IMessageDialog, YouMessageDialog>()，其中YouMessageDialog是你要自己实现的对话框，其他功能思想类似

### 模板
VS=>项目=>导出模板
#### Main
HeBianGu.Template.Main

#### Link
HeBianGu.Template.Link

## NuGet包

| **名称** |      **NuGet**      |
|----------|:-------------|
| **HeBianGu.Base.WpfBase** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Base.WpfBase)](https://www.nuget.org/packages/HeBianGu.Base.WpfBase)** |
| **HeBianGu.General.WpfControlLib** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.General.WpfControlLib)](https://www.nuget.org/packages/HeBianGu.General.WpfControlLib)** |
| **HeBianGu.Service.Animation** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Service.Animation)](https://www.nuget.org/packages/HeBianGu.Service.Animation)** |
| **HeBianGu.Service.AppConfig** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Service.AppConfig)](https://www.nuget.org/packages/HeBianGu.Service.AppConfig)** |
| **HeBianGu.Service.Converter** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Service.Converter)](https://www.nuget.org/packages/HeBianGu.Service.Converter)** |
| **HeBianGu.Service.License** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Service.License)](https://www.nuget.org/packages/HeBianGu.Service.License)** |
| **HeBianGu.Service.MarkupExtension** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Service.MarkupExtension)](https://www.nuget.org/packages/HeBianGu.Service.MarkupExtension)** |
| **HeBianGu.Service.Mvc** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Service.Mvc)](https://www.nuget.org/packages/HeBianGu.Service.Mvc)** |
| **HeBianGu.Service.Mvp** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Service.Mvp)](https://www.nuget.org/packages/HeBianGu.Service.Mvp)** |
| **HeBianGu.Service.TypeConverter** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Service.TypeConverter)](https://www.nuget.org/packages/HeBianGu.Service.TypeConverter)** |
| **HeBianGu.Service.Validation** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Service.Validation)](https://www.nuget.org/packages/HeBianGu.Service.Validation)** |
| **HeBianGu.Service.Image** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Service.Image)](https://www.nuget.org/packages/HeBianGu.Service.Image)** |
| **HeBianGu.Common.Expression** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Common.Expression)](https://www.nuget.org/packages/HeBianGu.Common.Expression)** |
| **HeBianGu.Control.Adorner** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.Adorner)](https://www.nuget.org/packages/HeBianGu.Control.Adorner)** |
| **HeBianGu.Control.Chart2D** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.Chart2D)](https://www.nuget.org/packages/HeBianGu.Control.Chart2D)** |
| **HeBianGu.Control.Diagram** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.Diagram)](https://www.nuget.org/packages/HeBianGu.Control.Diagram)** |
| **HeBianGu.Control.Drawer** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.Drawer)](https://www.nuget.org/packages/HeBianGu.Control.Drawer)** |
| **HeBianGu.Control.Explorer** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.Explorer)](https://www.nuget.org/packages/HeBianGu.Control.Explorer)** |
| **HeBianGu.Control.Filter** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.Filter)](https://www.nuget.org/packages/HeBianGu.Control.Filter)** |
| **HeBianGu.Control.GridSplitter** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.GridSplitter)](https://www.nuget.org/packages/HeBianGu.Control.GridSplitter)** |
| **HeBianGu.Control.ImagePlayer** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.ImagePlayer)](https://www.nuget.org/packages/HeBianGu.Control.ImagePlayer)** |
| **HeBianGu.Control.LeftMenu** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.LeftMenu)](https://www.nuget.org/packages/HeBianGu.Control.LeftMenu)** |
| **HeBianGu.Control.Host** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.Host)](https://www.nuget.org/packages/HeBianGu.Control.Host)** |
| **HeBianGu.Control.Message** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.Message)](https://www.nuget.org/packages/HeBianGu.Control.Message)** |
| **HeBianGu.Control.MessageContainer** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.MessageContainer)](https://www.nuget.org/packages/HeBianGu.Control.MessageContainer)** |
| **HeBianGu.Control.MessageListBox** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.MessageListBox)](https://www.nuget.org/packages/HeBianGu.Control.MessageListBox)** |
| **HeBianGu.Control.MultiComboBox** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.MultiComboBox)](https://www.nuget.org/packages/HeBianGu.Control.MultiComboBox)** |
| **HeBianGu.Control.PagedDataGrid** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.PagedDataGrid)](https://www.nuget.org/packages/HeBianGu.Control.PagedDataGrid)** |
| **HeBianGu.Control.Panel** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.Panel)](https://www.nuget.org/packages/HeBianGu.Control.Panel)** |
| **HeBianGu.Control.PasswordBox** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.PasswordBox)](https://www.nuget.org/packages/HeBianGu.Control.PasswordBox)** |
| **HeBianGu.Control.Ping** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.Ping)](https://www.nuget.org/packages/HeBianGu.Control.Ping)** |
| **HeBianGu.Control.PropertyGrid** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.PropertyGrid)](https://www.nuget.org/packages/HeBianGu.Control.PropertyGrid)** |
| **HeBianGu.Control.ScrollInto** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.ScrollInto)](https://www.nuget.org/packages/HeBianGu.Control.ScrollInto)** |
| **HeBianGu.Control.ScrollVewerLocator** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.ScrollVewerLocator)](https://www.nuget.org/packages/HeBianGu.Control.ScrollVewerLocator)** |
| **HeBianGu.Control.Shape** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.Shape)](https://www.nuget.org/packages/HeBianGu.Control.Shape)** |
| **HeBianGu.Control.Shuttle** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.Shuttle)](https://www.nuget.org/packages/HeBianGu.Control.Shuttle)** |
| **HeBianGu.Control.Step** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.Step)](https://www.nuget.org/packages/HeBianGu.Control.Step)** |
| **HeBianGu.Control.StoryBoard** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.StoryBoard)](https://www.nuget.org/packages/HeBianGu.Control.StoryBoard)** |
| **HeBianGu.Control.ThemeSet** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.ThemeSet)](https://www.nuget.org/packages/HeBianGu.Control.ThemeSet)** |
| **HeBianGu.Control.ToggleExpander** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.ToggleExpander)](https://www.nuget.org/packages/HeBianGu.Control.ToggleExpander)** |
| **HeBianGu.Control.TopContainer** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.TopContainer)](https://www.nuget.org/packages/HeBianGu.Control.TopContainer)** |
| **HeBianGu.Control.TreeListView** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.TreeListView)](https://www.nuget.org/packages/HeBianGu.Control.TreeListView)** |
| **HeBianGu.Control.Vlc** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Control.Vlc)](https://www.nuget.org/packages/HeBianGu.Control.Vlc)** |
| **HeBianGu.Window.Blur** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Window.Blur)](https://www.nuget.org/packages/HeBianGu.Window.Blur)** |
| **HeBianGu.Window.Float** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Window.Float)](https://www.nuget.org/packages/HeBianGu.Window.Float)** |
| **HeBianGu.Window.Link** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Window.Link)](https://www.nuget.org/packages/HeBianGu.Window.Link)** |
| **HeBianGu.Window.Login** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Window.Login)](https://www.nuget.org/packages/HeBianGu.Window.Login)** |
| **HeBianGu.Window.Menu** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Window.Menu)](https://www.nuget.org/packages/HeBianGu.Window.Menu)** |
| **HeBianGu.Window.Message** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Window.Message)](https://www.nuget.org/packages/HeBianGu.Window.Message)** |
| **HeBianGu.Window.MessageDialog** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Window.MessageDialog)](https://www.nuget.org/packages/HeBianGu.Window.MessageDialog)** |
| **HeBianGu.Window.Notify** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Window.Notify)](https://www.nuget.org/packages/HeBianGu.Window.Notify)** |
| **HeBianGu.Window.Start** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Window.Start)](https://www.nuget.org/packages/HeBianGu.Window.Start)** |
| **HeBianGu.Systems.Component** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Systems.Component)](https://www.nuget.org/packages/HeBianGu.Systems.Component)** |
| **HeBianGu.Systems.Identity** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Systems.Identity)](https://www.nuget.org/packages/HeBianGu.Systems.Identity)** |
| **HeBianGu.Systems.Project** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Systems.Project)](https://www.nuget.org/packages/HeBianGu.Systems.Project)** |
| **HeBianGu.Systems.Repository** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Systems.Repository)](https://www.nuget.org/packages/HeBianGu.Systems.Repository)** |
| **HeBianGu.Systems.Setting** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Systems.Setting)](https://www.nuget.org/packages/HeBianGu.Systems.Setting)** |
| **HeBianGu.Systems.Upgrade** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Systems.Upgrade)](https://www.nuget.org/packages/HeBianGu.Systems.Upgrade)** |
| **HeBianGu.Systems.XmlSerialize** | **[![NuGet](https://buildstats.info/nuget/HeBianGu.Systems.XmlSerialize)](https://www.nuget.org/packages/HeBianGu.Systems.XmlSerialize)** |

## 作者
<a href="https://github.com/HeBianGu" target="_blank"><img style="height:auto;" alt="" width="40" height="40" class="avatar avatar-user width-full border color-bg-default" src="https://avatars.githubusercontent.com/u/20257332?v=4"></a>

## 赞助支持 

### 支付宝
<img align="center" src="https://github.com/HeBianGu/WPF-ControlBase/blob/master/Document/Resource/z.jpg" width="200"/>

###  微信
<img align="center" src="https://github.com/HeBianGu/WPF-ControlBase/blob/master/Document/Resource/w.png" width="210"/>

## 博客
[https://blog.csdn.net/u010975589?type=blog](https://blog.csdn.net/u010975589?type=blog)



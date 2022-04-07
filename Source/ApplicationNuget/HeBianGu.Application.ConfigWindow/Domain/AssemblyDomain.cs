using HeBianGu.Base.WpfBase;
using HeBianGu.Common.LocalConfig;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Application.ConfigWindow
{
    public class AssemblyDomain : IAssemblyDomain
    { 
        public TestConfig LoadTestConfig(string path)
        {
            TestConfig testConfig = new TestConfig();
            testConfig = testConfig.LoadFromFile(path);
            return testConfig; 
        }

        public bool SaveTestConfig(TestConfig config,string path)
        {
            config.SaveToFile(path);

            return true;
        }


        public bool SaveTestConfigTest()
        {

            TestConfig config = new TestConfig();

            for (int i = 0; i < 3; i++)
            {
                TestCategory category = new TestCategory();
                category.Name = "功率" + i;
                config.TestCategories.Add(category);

                for (int j = 0; j < 10; j++)
                {
                    Item ti = new Item();
                    ti.Name = "Power" + j;
                    ti.ShowName = "Power" + j;
                    ti.DLLName = "Power" + j;
                    ti.ClassName = "PowerTest" + j;
                    InstrumentCfg testInstrumentConfig = new InstrumentCfg();
                    testInstrumentConfig.Name = "N5244B" + j;
                    testInstrumentConfig.InstrumentType = "VNA" + j;
                    Option option = new Option();
                    option.Name = "变频时延" + j;
                    option.Value = 1;
                    testInstrumentConfig.Options.Add(option);
                    ti.Instruments.Add(testInstrumentConfig);

                    for (int ii = 0; ii < 4; ii++)
                    {
                        Parameter paras = new Parameter()
                        {
                            Name = "StartFrequency" + ii,
                            DefaultValue = "1000",
                            ParaType = EDataType.Number,
                            ShowName = "起始频率" + ii,
                            Unit = "Hz",
                            Visible = true
                        };
                        CompareCondition up = new CompareCondition()
                        {
                            Compare = ECompareType.LessAndEqual,
                            Value = 10000,
                        };
                        CompareCondition down = new CompareCondition()
                        {
                            Compare = ECompareType.GraetThan,
                            Value = 1000,
                        };
                        paras.Conditions.Add(up);
                        paras.Conditions.Add(down);
                        ti.Parameters.Add(paras);
                    }

                    for (int iii = 0; iii < 1; iii++)
                    {
                        Limit limit = new Limit()
                        {
                            Name = "Power" + iii,
                            LimitType = EDataType.Number,
                            ShowName = "功率" + iii,
                            Unit = "db",
                            Visible = true,
                        };
                        CompareCondition lup = new CompareCondition()
                        {
                            Compare = ECompareType.LessAndEqual,
                            Value = 10000,
                        };
                        CompareCondition ldown = new CompareCondition()
                        {
                            Compare = ECompareType.GraetThan,
                            Value = 1000,
                        };
                        limit.CompareConditions.Add(ldown);
                        limit.CompareConditions.Add(lup);
                        ti.Limits.Add(limit);
                    }
                    category.Items.Add(ti);
                }
            } 
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace wpfTest.MyTool
{
    [Serializable]
    public class AllTools
    {
        public Dictionary<string, Tool> allTools = new Dictionary<string, Tool>();
        //public List<Tool> tools = new List<Tool>();
        public void addTool(Tool anTool)
        {
            allTools.Add(anTool.myButton.Content.ToString(), anTool);
        }
        public void removeTool(string _buttonName)
        {
            if (allTools.ContainsKey(_buttonName))
            {
                allTools.Remove(_buttonName);
            }
            else
            {
                MessageBox.Show("此快捷方式不存在");

            }

        }

    }

    [Serializable]
    public class Tool
    {
        // public string title;
        [NonSerialized]
        public Button myButton;
        public string buttonName;
        public string buttonPath;
        [NonSerialized]
        public WrapPanel containerPanel;
        public int useTime = 0;

        //public void ShowButton()
        //{
        //    //if (!containerPanel.Children.Contains(myButton))
        //    //{

        //    containerPanel.Children.Add(myButton);
        //    //}

        //}
        void ButtonClick()
        {

        }
        public Tool(Button _myButton, string _buttonPath, WrapPanel _containerPanel, int _useTime)
        {
            myButton = _myButton;
            buttonPath = _buttonPath;
            buttonName = myButton.Content.ToString();
            useTime = _useTime;
            myButton.Click += new System.Windows.RoutedEventHandler(btn_click);
        }
        private void btn_click(object sender, RoutedEventArgs e)
        {

           

            if (buttonPath != "" && buttonPath != null)
            {
                // string filePath = buttonPath.Replace(buttonName, "");
                if (File.Exists(buttonPath) || Directory.Exists(buttonPath))
                {

                    System.Diagnostics.Process.Start("explorer.exe", buttonPath);

                }
                else
                {
                    MessageBox.Show(buttonPath + ":文件不存在或已被删除！！！");
                }
            }
            else
            {
                MessageBox.Show("没有指定路径！！！");
                return;

            }
            useTime += 1;
        }

    }


    class DataForever
    {




        public string infoPath = "";

        public AllTools oldAllTools;

        public DataForever(string _infoPath)
        {
            infoPath = _infoPath;
            string[] strs = infoPath.Split('\\');
            string fileString = infoPath.Replace("\\" + strs[strs.Length - 1], "");
            if (Directory.Exists(fileString) == false)//如果不存在就创建file文件夹{
                Directory.CreateDirectory(fileString);
        }



        //读取玩家的数据
        public AllTools LoadPlayerData()
        {


            //如果路径上有文件，就读取文件
            if (File.Exists(infoPath))
            {


                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(infoPath, FileMode.Open);
                //读取数据
                if (file.Length == 0)
                {
                    oldAllTools = new AllTools();
                }
                else
                {
                    try
                    {
                        oldAllTools = (AllTools)bf.Deserialize(file);

                    }
                    catch
                    {
                        //Window mainW =Application.Current.MainWindow;
                        //bool isTopMost = mainW.Topmost;
                        //mainW.Topmost = false;
                        MessageBox.Show("反序列化出错！");
                        //mainW.Topmost = isTopMost;
                        oldAllTools = new AllTools();

                    }
                    file.Close();
                }
            }
            //如果没有文件，就new出一个PlayerData
            else
            {
                oldAllTools = new AllTools();

            }
            return oldAllTools;
        }

        //保存玩家的数据
        public void SavePlayerData(AllTools newallTools)
        {
            //allTools = new TestDate(CreatePingCe.Datas);
            //保存数据      
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(infoPath))
            {
                File.Delete(infoPath);
            }
            FileStream file = File.Create(infoPath);
            try
            {

                bf.Serialize(file, newallTools);

            }
            catch
            {
                //Window mainW = Application.Current.MainWindow;
                //bool isTopMost = mainW.Topmost;
                //mainW.Topmost = false;
                MessageBox.Show("序列化出错！");
                //mainW.Topmost = isTopMost;


            }
            file.Close();

        }


    }
}

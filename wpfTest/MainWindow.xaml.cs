using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wpfTest.MyClass;
using wpfTest.MyTool;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
using Demo;
using Mouse = Demo.Mouse;
using Keyboard = System.Windows.Input.Keyboard;

namespace wpfTest
{






    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        //快捷键功能设置
        #region
        protected override void OnSourceInitialized(EventArgs e)
        {

            Hotkey.Regist(this, HotkeyModifiers.MOD_CONTROL, Key.OemTilde, () =>
            {
                ShowTab(1);
            });
            Hotkey.Regist(this, HotkeyModifiers.MOD_CONTROL, Key.D1, () =>
            {
                ShowTab(2);
            });
            Hotkey.Regist(this, HotkeyModifiers.MOD_CONTROL, Key.D2, () =>
            {
                ShowTab(3);
            });
            Hotkey.Regist(this, HotkeyModifiers.MOD_CONTROL, Key.D3, () =>
            {
                ShowTab(4);
            });
            Hotkey.Regist(this, HotkeyModifiers.MOD_CONTROL, Key.D4, () =>
            {
                ShowTab(5);
            });
            Hotkey.Regist(this, HotkeyModifiers.MOD_CONTROL, Key.D5, () =>
            {
                ShowTab(6);
            });
            Hotkey.Regist(this, HotkeyModifiers.MOD_CONTROL, Key.D6, () =>
            {
                ShowTab(7);
            });
            Hotkey.Regist(this, HotkeyModifiers.MOD_CONTROL, Key.D7, () =>
            {
                ShowTab(8);
            });
            Hotkey.Regist(this, HotkeyModifiers.MOD_CONTROL, Key.D8, () =>
            {
                ShowTab(9);
            });

            Hotkey.Regist(this, HotkeyModifiers.MOD_CONTROL, Key.Q, () =>
            {
                WindowState = WindowState.Minimized;
            });
            //Hotkey.Regist(this, HotkeyModifiers.MOD_CONTROL, Key.B, () =>
            //{
            //    MaxAndMiniWindow();
            //});
            //Hotkey.Regist(this, HotkeyModifiers.MOD_ALT, Key.Z, () =>
            //{
            //    MakeMiddleMouseButoonDown();
            //});

        }

        //private void MakeMiddleMouseButoonDown()
        //{
        //    Mouse.Click(MouseButton.Middle);
        //}

        //呼出窗口并且显示所有窗口
        private void ShowTab(int pageNum)
        {

            switch (pageNum)
            {
                case 1:
                    WindowState = WindowState.Normal;
                    myTabControl.SelectedIndex = 0;
                    return;
                case 2:
                    WindowState = WindowState.Normal;
                    myTabControl.SelectedIndex = 1;
                    DragTabs.SelectedIndex = 0;
                    return;
                case 3:
                    WindowState = WindowState.Normal;
                    myTabControl.SelectedIndex = 1;
                    DragTabs.SelectedIndex = 1;
                    return;
                case 4:
                    WindowState = WindowState.Normal;
                    myTabControl.SelectedIndex = 1;
                    DragTabs.SelectedIndex = 2;
                    return;
                case 5:
                    WindowState = WindowState.Normal;
                    myTabControl.SelectedIndex = 0;
                    myTabControl.SelectedIndex = 1;
                    DragTabs.SelectedIndex = 3;
                    return;
                case 6:
                    WindowState = WindowState.Normal;
                    myTabControl.SelectedIndex = 1;
                    DragTabs.SelectedIndex = 4;
                    return;
                case 7:
                    WindowState = WindowState.Normal;
                    myTabControl.SelectedIndex = 1;
                    DragTabs.SelectedIndex = 5;
                    return;
                case 8:
                    WindowState = WindowState.Normal;
                    myTabControl.SelectedIndex = 2;
                    return;
                case 9:
                    WindowState = WindowState.Normal;
                    myTabControl.SelectedIndex = 3;
                    return;
                default:
                    return;


            }

        }

        //呼出隐藏窗口
        private void MaxAndMiniWindow()
        {
            //MessageBox.Show("");
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
            }
            else if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Minimized;
            }
        }
        #endregion



        /// <summary>
        ///全局私有变量&初始化函数&按钮生成函数
        ///*************************************************************************************************************************************
        /// <summary>

        //初始化检索    
        #region
        private void ContrastUserData()
        {
            BackUpUserData();
            string iniWords1 = SetShortCutTool();
            string iniWords2 = SetDragTool();
            string iniFinishWords = iniWords1 + iniWords2;
            if (iniFinishWords != "")
            {
                //bool isTopMost = this.Topmost;
                //this.Topmost = false;
                MessageBox.Show(iniFinishWords, "数据比对报告");
                //this.Topmost = isTopMost;

            }

        }
        //备份UserDate
        private void BackUpUserData()
        {
            string backupFilesPath = rootPath + @"UnityBox\Backup\HistoryDatas";
            if (Directory.Exists(backupFilesPath) == false)//如果不存在就创建file文件夹{
            {

                Directory.CreateDirectory(backupFilesPath);

            }
            string oringinalFilePath = rootPath + @"UnityBox\UserData";
            if (Directory.Exists(oringinalFilePath) == false)//如果不存在就创建UserData文件夹{
            {

                Directory.CreateDirectory(oringinalFilePath);
                return;
            }

            if (Directory.GetDirectories(oringinalFilePath).Length > 0 || Directory.GetFiles(oringinalFilePath).Length > 0)
            {
                string nowTime = DateTime.Now.ToString("yyyy年MM月dd日h时mm分ss秒");
                string aimDir = backupFilesPath + "\\" + nowTime;
                //Directory.CreateDirectory(aimDir);

                CopyDirectory(oringinalFilePath, aimDir + "\\UserData");
            }
        }
        #endregion

        //数据文件夹存放目录（rootPath），结尾需要带“\”,获取窗口名
        #region
        public static string rootPath;
        string winTitle = "Unity百宝箱1.0-------by知行合一";

        void SetRootPath()
        {
            try

            {
                string exePath = Directory.GetCurrentDirectory() + "\\";

                //按照日期建立一个文件名
                string txtPath = exePath + "RootPathInfo" + ".txt";

                //判断文件的存在
                if (System.IO.File.Exists(txtPath))
                {
                    //存在文件
                    string strData = "";
                    try
                    {
                        string line;
                        // 创建一个 StreamReader 的实例来读取文件 ,using 语句也能关闭 StreamReader
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(txtPath))
                        {
                            // 从文件读取并显示行，直到文件的末尾
                            while ((line = sr.ReadLine()) != null)
                            {
                                //Console.WriteLine(line);
                                strData = line;
                            }
                        }
                        rootPath = strData;
                    }
                    catch (Exception e)
                    {
                        // 向用户显示出错消息
                        //Console.WriteLine("The file could not be read:");
                        //Console.WriteLine(e.Message);
                        //bool isTopMost = this.Topmost;
                        //this.Topmost = false;
                        MessageBox.Show("读取root地址错误,程序即将退出，请查看：" + txtPath + "文本内容，路径是否正确");
                        //this.Topmost = isTopMost;
                        Environment.Exit(0);


                    }

                    if (Directory.Exists(rootPath))
                    {
                        return;
                    }
                    else
                    {
                        //bool isTopMost = this.Topmost;
                        //this.Topmost = false;
                        MessageBox.Show("读取root地址错误,程序即将退出，不存在路径：" + rootPath);
                        //this.Topmost = isTopMost;
                        Environment.Exit(0);
                    }


                }
                else
                {
                    //文件覆盖方式添加内容
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(txtPath, false);
                    //保存数据到文件,将root地址写入此文本
                    rootPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
                    sw.Write(rootPath);
                    //关闭文件
                    sw.Close();
                    //释放对象
                    sw.Dispose();
                }




                //rootPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
                // rootPath= Directory.GetCurrentDirectory();
                // myRichBox.AppendText(rootPath);

            }
            catch
            {

                // myRichBox.AppendText("获取目录出错");
            }
        }
        #endregion

        //获取拖拽素材模块对象（dragToolDf），比对整合文件目录和序列化数据“DragToolInfo”，获取myAllDragTools，并且初始化生成按钮
        #region
        DataForever dragToolDf;
        public static AllTools myAllDragTools;
        //制作一个dragTool,加入或更新myAllDragTools的tool，返回新增的文件路径
        String MakeDragButton(string _path, string _name, DataForever _dragToolDf)
        {

            string newToolPath = "";
            string picPath;
            WrapPanel myContainerPanel;
            Brush foreColorBrush;
            string[] forExt = _name.Split('.');

            if (forExt.Length > 1)
            {
                string myExt = forExt[forExt.Length - 1];
                switch (myExt)
                {
                    case "unitypackage":
                        picPath = @"Resources\PKG.jpg";
                        myContainerPanel = PackagesPanel;
                        foreColorBrush = Brushes.White;
                        break;
                    case "cs":
                        picPath = @"Resources\Script.jpg";
                        myContainerPanel = ScriptsPanel;
                        foreColorBrush = Brushes.Black;
                        break;
                    case "shader":
                        picPath = @"Resources\Shader.jpg";
                        myContainerPanel = ShadersPanel;
                        foreColorBrush = Brushes.White;
                        break;
                    case "pdf":
                        picPath = @"Resources\Book.jpg";
                        myContainerPanel = BooksPanel;
                        foreColorBrush = Brushes.White;

                        break;
                    default:
                        picPath = @"Resources\Other.jpg";
                        myContainerPanel = OthersPanel;
                        foreColorBrush = Brushes.Black;
                        break;


                }
            }
            else
            {

                picPath = @"Resources\DragFolder.jpg";
                myContainerPanel = FoldersPanel;
                foreColorBrush = Brushes.Black;
            }

            Uri uri = new Uri(picPath, UriKind.Relative);
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(uri);

            var converter = new System.Windows.Media.BrushConverter();
            Button btn = new Button
            {
                Name = "tool",
                Content = _name,
                Width = 137.5,
                Height = 40,
                // Background= (System.Windows.Media.Brushes)("#FFFFE79E")
                Background = ib,
                Foreground = foreColorBrush,
                AllowDrop = true,
                Visibility = Visibility.Visible,
            };
            Tool oneTool = new Tool(btn, _path, myContainerPanel, 0);
            //设置鼠标按下事件
            btn.AddHandler(Button.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler((sender, e) =>
            {
                bool isTopMost = this.Topmost;
                if (e.ClickCount == 1)
                {
                    this.Topmost = false;

                }
                Brush oldBrush = btn.Background;
                btn.Background = Brushes.Green;
                ////自写代码
                FileDDWatcher.Instance.StartWatcher(1);// 启动文件挪移监控,传入文件挪移的个数
                string dir = rootPath + @"UnityBox\DragToolPath\" + btn.Content.ToString();
                // myRichBox.AppendText("dragOutContent:" + btn.Content.ToString());

                string[] paths = new string[] { dir };



                #region 拖拽代码

                ListView lv = new ListView();
                string dataFormat = DataFormats.FileDrop;
                DataObject dataObject = new DataObject(dataFormat, paths);
                DragDropEffects dde = DragDrop.DoDragDrop(lv, dataObject, DragDropEffects.Copy);

                #endregion
                FileDDWatcher.Instance.StopWatcher();// 停止文件监控

                #region 拖拽成功之后获取目标的路径
                Console.WriteLine($"dropdrag status = {dde}");
                if (dde == DragDropEffects.Copy)// 拖拽成功
                {
                    string[] ls = FileDDWatcher.Instance.GetTempFilePath();// 获取拖拽之后的目录
                    if (ls != null && ls.Length > 0)
                    {
                        for (int i = 0; i < ls.Length; i++)
                        {
                            // myRichBox.AppendText($"Target catalogue {ls[i]}");

                        }
                    }
                    else
                    {
                        // myRichBox.AppendText("no file path copy");

                    }

                    btn.Background = oldBrush;
                }

                oneTool.useTime += 1;

                //  myContainerPanel.Children.Add(btn);

                #endregion

                //dragOutContent = btn.Content.ToString();
                ////dragOutContent = (sender as Button).Name;
                //myRichBox.AppendText("\n" + dragOutContent + ":拖拽成功");
                this.Topmost = isTopMost;

            }), true);


            //设置鼠标双击事件
            btn.AddHandler(Button.MouseDoubleClickEvent, new MouseButtonEventHandler((sender, e) =>
            {
                if (e.ChangedButton != MouseButton.Left)
                {
                    return;
                }
                //myRichBox.AppendText("\n" + dragOutContent + ":拖拽结束");
                if (_path != "" && _path != null)
                {
                    // string filePath = buttonPath.Replace(buttonName, "");
                    if (System.IO.File.Exists(_path) || System.IO.Directory.Exists(_path))
                    {

                        System.Diagnostics.Process.Start("explorer.exe", _path);

                    }
                    else
                    {
                        //bool isTopMost = this.Topmost;
                        //this.Topmost = false;
                        MessageBox.Show(_path + ":此拖拽素材文件已经失去引用，你已转移或删除了此文件。");
                        //this.Topmost = isTopMost;
                    }
                }
                else
                {
                    //bool isTopMost = this.Topmost;
                    //this.Topmost = false;
                    MessageBox.Show("拖拽素材没有指定路径！");
                    //this.Topmost = isTopMost;
                    return;

                }
                oneTool.useTime += 1;

            }), true);
            //删除事件
            btn.MouseDown +=
                    (sender, e) =>
                    {

                        if (e.ChangedButton == MouseButton.Middle & e.ButtonState == MouseButtonState.Pressed)

                            if (Keyboard.IsKeyDown(Key.LeftCtrl))
                            {
                                //删除文件
                                try
                                {
                                    DeleteFolder(_path);

                                }
                                catch
                                {
                                    //bool isTopMost = this.Topmost;
                                    //this.Topmost = false;
                                    MessageBox.Show("删除失败，UnityBox\\Backup\\DeleteFiles存在同名文件,请先清理此文件。");
                                    //this.Topmost = isTopMost;
                                    return;
                                }

                                myAllDragTools.removeTool(btn.Content.ToString());
                                myContainerPanel.Children.Remove(btn);
                                //oneTool.ShowButton();
                                dragToolDf.SavePlayerData(myAllDragTools);
                            }
                            else
                            {
                                //bool isTopMost = this.Topmost;
                                //this.Topmost = false;
                                MessageBoxResult isGoToDelete = MessageBox.Show("是否真的要删除拖拽素材：" + btn.Content.ToString() + "?", "警告", MessageBoxButton.YesNo, MessageBoxImage.Information);
                                //this.Topmost = isTopMost;
                                if (isGoToDelete == MessageBoxResult.Yes)
                                {
                                    //删除文件
                                    try
                                    {
                                        DeleteFolder(_path);

                                    }
                                    catch
                                    {
                                        //isTopMost = this.Topmost;
                                        //this.Topmost = false;
                                        MessageBox.Show("删除失败，UnityBox\\Backup\\DeleteFiles存在同名文件,请先清理此文件。");
                                        //this.Topmost = isTopMost;
                                        return;
                                    }

                                    myAllDragTools.removeTool(btn.Content.ToString());
                                    myContainerPanel.Children.Remove(btn);
                                    //oneTool.ShowButton();
                                    dragToolDf.SavePlayerData(myAllDragTools);
                                }
                            }


                    };

            //显示tool信息事件1
            btn.MouseEnter +=
              (sender, e) =>
              {
                  UnityToolWindow.Title = "拖拽素材：" + btn.Content.ToString() + "---使用次数" + myAllDragTools.allTools[btn.Content.ToString()].useTime + "次";
              };
            //显示tool信息事件2
            btn.MouseLeave +=
                (sender, e) =>
                {
                    UnityToolWindow.Title = winTitle;
                };

            if (myAllDragTools.allTools.ContainsKey(_name))
            {
                oneTool.useTime = myAllDragTools.allTools[_name].useTime;
                myAllDragTools.allTools[_name] = oneTool;

            }
            else
            {
                myAllDragTools.addTool(oneTool);
                newToolPath = "\n" + oneTool.buttonPath + "\n";
            }
            myContainerPanel.Children.Add(btn);

            return newToolPath;
        }
        string SetDragTool()
        {
            //设定DragTool的信息文件目录，赋值（dragToolDf，myAllDragTools）
            dragToolDf = new DataForever(rootPath + @"UnityBox\UserData\DragToolInfo");
            myAllDragTools = dragToolDf.LoadPlayerData();

            //判断当前myAllDragTools内所有按钮信息是否为0，为0则不存在任何拖拽按钮，hasBottonInfo值设置为false
            bool noBottonInfo = myAllDragTools.allTools.Count == 0 ? true : false;

            //(1)
            //获取文件夹内所有路径,以文件名和按钮信息作为键值对，录入字典,字典已按照key字符排序（ GetAllDic(dragPath)）
            string dragPath = rootPath + @"UnityBox\DragToolPath";

            if (Directory.Exists(dragPath) == false)//如果不存在就创建file文件夹{
            {
                Directory.CreateDirectory(dragPath);

            }
            Dictionary<string, string> allDragPath = GetAllDic(dragPath);
            //判断文件夹内是否有文件，没有则noFilePath值为true;
            bool noFilePath = allDragPath.Count == 0 ? true : false;



            //如果hasBottonInfo和noFilePath的值同时为false，则判断按钮数据是否多余,如果多余就去除,记录多余数据
            string removedMsg = "";
            if (noFilePath == false)
            {

                if (noBottonInfo == false)
                {
                    List<string> removedKey = new List<string>();
                    foreach (KeyValuePair<string, Tool> kvp in myAllDragTools.allTools)
                    {
                        if (allDragPath.ContainsKey(kvp.Key) == false)
                        {

                            removedKey.Add(kvp.Key);
                            removedMsg += ("\n" + kvp.Value.buttonPath + "\n");
                        }
                    }
                    foreach (string k in removedKey)
                    {

                        myAllDragTools.allTools.Remove(k);
                    }
                }
            }
            else
            {
                if (noBottonInfo == false)
                {
                    List<string> removedKey = new List<string>();
                    foreach (KeyValuePair<string, Tool> kvp in myAllDragTools.allTools)
                    {
                        removedKey.Add(kvp.Key);
                        removedMsg += ("\n" + kvp.Value.buttonPath + "\n");
                    }
                    foreach (string k in removedKey)
                    {
                        myAllDragTools.allTools.Remove(k);
                    }
                }

            }


            //排序myAllDragTools.allTools,让它们进入链表（arrayTools）
            List<Tool> arrayTools = new List<Tool>();
            foreach (KeyValuePair<string, string> kvp in allDragPath)
            {
                if (myAllDragTools.allTools.ContainsKey(kvp.Key))
                {
                    Tool nowTool = myAllDragTools.allTools[kvp.Key];
                    if (arrayTools.Count == 0)
                    {
                        arrayTools.Add(nowTool);
                        continue;
                    }
                    int insertIndex = arrayTools.Count;
                    for (int j = arrayTools.Count - 1; j >= 0; j--)
                    {
                        if (nowTool.useTime > arrayTools[j].useTime)
                        {
                            insertIndex = j;
                        }
                        else
                        {
                            break;
                        }

                    }
                    arrayTools.Insert(insertIndex, nowTool);
                }
                else
                {
                    Button newBtn = new Button();
                    newBtn.Content = kvp.Key;
                    arrayTools.Add(new Tool(newBtn, kvp.Value, new WrapPanel(), 0));
                }
            }
            // MessageBox.Show(myAllDragTools.allTools.Count.ToString());

            //通过排序后的tool链表，显示按钮
            string addedMsg = "";
            foreach (Tool t in arrayTools)
            {
                //t.ShowButton();
                //生成所有按钮
                //foreach (KeyValuePair<string, string> kvp in allDragPath)
                //{
                addedMsg += MakeDragButton(t.buttonPath, t.buttonName, dragToolDf);
                //t.ShowButton();
                //}
            }
            //更新“DragToolInfo”
            dragToolDf.SavePlayerData(myAllDragTools);
            if (addedMsg != "" || removedMsg != "")
            {
                string showFinish = "";
                if (addedMsg != "")
                {
                    showFinish += "**************\n本次更新拖拽素材如下：";
                    showFinish += addedMsg;
                }
                if (removedMsg != "")
                {
                    showFinish += "**************\n失去以下拖拽素材：\n（你已经手动删除或转移有关文件或者正在数据还原）";
                    showFinish += removedMsg;

                }

                return (showFinish);
            }

            return "";
        }
        #endregion

        //获取快捷方式素材模块对象（shortCutToolDf），检查序列化数据“DragToolInfo”，判断丢失文件有哪些，并且初始化生成按钮
        #region
        DataForever shortCutToolDf;
        public static AllTools myAllShortCutTools;

        public bool Visible { get; private set; }

        //检索控件是否失去引用，为存在引用的数据制作一个tool，赋给myAllShortCutTools，返回失去引用的文件路径
        string MakeShortcutButton(string _buttonName)
        {
            //判断文件引用是否还存在，不存在
            if (File.Exists(myAllShortCutTools.allTools[_buttonName].buttonPath) == false && Directory.Exists(myAllShortCutTools.allTools[_buttonName].buttonPath) == false)
            {
                string rvPath = myAllShortCutTools.allTools[_buttonName].buttonPath;
                myAllShortCutTools.allTools.Remove(_buttonName);
                return "\n" + rvPath + "\n";
            }

            Uri uri = new Uri(@"Resources\快捷导向.jpg", UriKind.Relative);
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(uri);
            Button btn = new Button
            {
                Name = "shortCutTool",
                Foreground = Brushes.Black,
                Content = _buttonName,
                Width = 100,
                Height = 40,
                Background = ib,
                Visibility = Visibility.Visible,
                AllowDrop = true
            };

            //btn.MouseDown +=
            //    (sender, e) =>
            //    {

            //        if (e.ChangedButton == MouseButton.Middle & e.ButtonState == MouseButtonState.Pressed)

            //            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            //            {
            //                myAllShortCutTools.removeTool(btn.Content.ToString());
            //                this.ShortcutsPanel.Children.Remove(btn);
            //                shortCutToolDf.SavePlayerData(myAllShortCutTools);
            //            }
            //            else
            //            {

            //                MessageBoxResult isGoToDelete = MessageBox.Show("是否真的要删除快捷方式：" + btn.Content.ToString() + "?\n您可以先备份桌面文件夹UnityBox\\UserData\\ShortCutToolInfo文件\n否者将不可逆！", "警告", MessageBoxButton.YesNo, MessageBoxImage.Information);
            //                if (isGoToDelete == MessageBoxResult.Yes)
            //                {
            //                    myAllShortCutTools.removeTool(btn.Content.ToString());
            //                    this.ShortcutsPanel.Children.Remove(btn);
            //                    shortCutToolDf.SavePlayerData(myAllShortCutTools);
            //                }
            //            }


            //    };

            //删除事件
            btn.MouseDown +=
                (sender, e) =>
                {
                    if (e.ChangedButton == MouseButton.Middle & e.ButtonState == MouseButtonState.Pressed)

                        if (Keyboard.IsKeyDown(Key.LeftCtrl))
                        {
                            myAllShortCutTools.removeTool(btn.Content.ToString());
                            this.ShortcutsPanel.Children.Remove(btn);
                            shortCutToolDf.SavePlayerData(myAllShortCutTools);
                        }
                        else
                        {
                            //bool isTopMost = this.Topmost;
                            //this.Topmost = false;
                            MessageBoxResult isGoToDelete = MessageBox.Show("是否真的要删除快捷方式：" + btn.Content.ToString() + "?", "警告", MessageBoxButton.YesNo, MessageBoxImage.Information);
                            //this.Topmost = isTopMost;
                            if (isGoToDelete == MessageBoxResult.Yes)
                            {
                                myAllShortCutTools.removeTool(btn.Content.ToString());
                                this.ShortcutsPanel.Children.Remove(btn);
                                shortCutToolDf.SavePlayerData(myAllShortCutTools);
                            }
                        }
                };

            //显示工具信息1
            btn.MouseEnter +=
                (sender, e) =>
                {
                    UnityToolWindow.Title = "快捷方式：" + btn.Content.ToString() + "---使用次数" + myAllShortCutTools.allTools[btn.Content.ToString()].useTime + "次";
                };
            //显示工具信息2
            btn.MouseLeave +=
                (sender, e) =>
                {
                    UnityToolWindow.Title = winTitle;
                };
            if (myAllShortCutTools.allTools.ContainsKey(_buttonName))
            {
                Tool oneTool = new Tool(btn, myAllShortCutTools.allTools[_buttonName].buttonPath, ShortcutsPanel, myAllShortCutTools.allTools[_buttonName].useTime);
                myAllShortCutTools.allTools[_buttonName] = oneTool;
            }
            else
            {
                Tool oneTool = new Tool(btn, myAllShortCutTools.allTools[_buttonName].buttonPath, ShortcutsPanel, 0);
                myAllShortCutTools.addTool(oneTool);
            }



            ShortcutsPanel.Children.Add(btn);
            return "";
        }
        private string SetShortCutTool()
        {
            //赋值（shortCutToolDf，myAllShortCutTools）
            shortCutToolDf = new DataForever(rootPath + @"UnityBox\UserData\ShortcutToolInfo");
            myAllShortCutTools = shortCutToolDf.LoadPlayerData();

            //按钮名字排序
            List<string> myShortCutNames = new List<string>();
            myAllShortCutTools.allTools = (from entry in myAllShortCutTools.allTools
                                           orderby entry.Key ascending
                                           select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
            //按钮使用次数排序，获取所有按钮的名字
            foreach (KeyValuePair<string, Tool> kvp in myAllShortCutTools.allTools)
            {
                if (myShortCutNames.Count == 0)
                {
                    myShortCutNames.Add(kvp.Key);
                    continue;
                }
                else
                {
                    int insertIndex = myShortCutNames.Count;
                    for (int i = myShortCutNames.Count - 1; i >= 0; i--)
                    {

                        if (myAllShortCutTools.allTools[kvp.Key].useTime > myAllShortCutTools.allTools[myShortCutNames[i]].useTime)
                        {
                            insertIndex = i;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (insertIndex == myShortCutNames.Count)
                    {

                        myShortCutNames.Add(kvp.Key);
                    }
                    else
                    {
                        myShortCutNames.Insert(insertIndex, kvp.Key);
                    }
                }
            }


            //foreach生成按钮，并记录失去引用的文件路径
            string noPathMsg = "";
            foreach (string buttonName in myShortCutNames)
            {
                noPathMsg += MakeShortcutButton(buttonName);
                //if (myAllShortCutTools.allTools.ContainsKey(buttonName))
                //{
                //    myAllShortCutTools.allTools[buttonName].ShowButton();

                //}
            }
            shortCutToolDf.SavePlayerData(myAllShortCutTools);
            if (noPathMsg != "")
            {
                string finishMsg = "**************\n已清除失去引用的快捷方式，这些快捷方式的路径为以下：" + noPathMsg;
                return finishMsg;
            }
            return "";

        }
        #endregion

        /// <summary>
        ///全局私有变量&初始化函数&按钮生成函数
        ///*************************************************************************************************************************************
        /// <summary>





        public MainWindow()
        {



            //窗口初始化
            InitializeComponent();
            //设置数据文件夹存放目录
            SetRootPath();
            //数据比对
            ContrastUserData();
            ////注册快捷键
            //SetKeyControl();

        }







        /// <summary>
        ///内拖拽功能
        ///*************************************************************************************************************************************
        /// <summary>
        #region
        private void Unity百宝箱_Drop(object sender, DragEventArgs e)
        {

            string path = "";
            string fileName = "";
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                Array myArray = (System.Array)e.Data.GetData(DataFormats.FileDrop);
                path = myArray.GetValue(0).ToString();
                fileName = System.IO.Path.GetFileName(path);
            }
            string nowTabName = ((TabItem)myTabControl.SelectedItem).Name;
            //MessageBox.Show(fileName);

            switch (nowTabName)
            {
                case "Shortcuts":
                    MakeShortcutTool(fileName, path);
                    return;
                case "DragItems":
                    MakeDragTool(path, fileName);
                    return;
                default:
                    return;
            }
        }
        //函数----新加一个dragTool
        void MakeDragTool(string originalToolPath, string name)
        {
            if (myAllDragTools.allTools.ContainsKey(name))
            {
                // MessageBox.Show("新建拖拽素材失败，已有同名工具");
                return;
            }
            //string name = System.IO.Path.GetFileName(originalToolPath);
            //string nameEx = System.IO.Path.GetFileNameWithoutExtension(originalToolPath);
            string picPath;
            WrapPanel myContainerPanel;
            Brush foreColorBrush;
            TabItem aimTab;
            string[] forExt = name.Split('.');

            if (forExt.Length > 1)
            {
                string myExt = forExt[forExt.Length - 1];
                switch (myExt)
                {
                    case "unitypackage":
                        picPath = @"Resources\PKG.jpg";
                        myContainerPanel = PackagesPanel;
                        foreColorBrush = Brushes.White;
                        aimTab = PackagesTab;
                        break;
                    case "cs":
                        picPath = @"Resources\Script.jpg";
                        myContainerPanel = ScriptsPanel;
                        foreColorBrush = Brushes.Black;
                        aimTab = ScriptsTab;
                        break;
                    case "shader":
                        picPath = @"Resources\Shader.jpg";
                        myContainerPanel = ShadersPanel;
                        foreColorBrush = Brushes.White;
                        aimTab = ShadersTab;
                        break;
                    case "pdf":
                        picPath = @"Resources\Book.jpg";
                        myContainerPanel = BooksPanel;
                        foreColorBrush = Brushes.White;
                        aimTab = BooksTab;
                        break;
                    default:
                        picPath = @"Resources\Other.jpg";
                        myContainerPanel = OthersPanel;
                        foreColorBrush = Brushes.Black;
                        aimTab = OthersTab;
                        break;


                }
            }
            else
            {

                picPath = @"Resources\DragFolder.jpg";
                myContainerPanel = FoldersPanel;
                foreColorBrush = Brushes.Black;
                aimTab = FoldersTab;
            }


            Uri uri = new Uri(picPath, UriKind.Relative);
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(uri);

            //  var converter = new System.Windows.Media.BrushConverter();
            Button btn = new Button
            {
                Name = "tool_new",
                Content = name,
                Width = 137.5,
                Height = 40,
                // Background= (System.Windows.Media.Brushes)("#FFFFE79E")
                Background = ib,
                Foreground = foreColorBrush,
                AllowDrop = true,
                Visibility = Visibility.Visible,
            };

            //先拷贝一下这份文件
            string pLocalFilePath = originalToolPath;//要复制的文件路径
            string pSaveFilePath = rootPath + @"UnityBox\DragToolPath";//指定存储的路径
            string nowPath = pSaveFilePath + "\\" + name;
            //if (File.Exists(pLocalFilePath))//必须判断要复制的文件是否存在
            //{
            try
            {
                CopyDirectory(pLocalFilePath, nowPath);
            }
            catch
            {
                //bool isTopMost = this.Topmost;
                //this.Topmost = false;
                MessageBox.Show("拷贝失败！或许关于此文件的同名文件已存在。");
                //this.Topmost = isTopMost;
                return;

            }
            Tool oneTool = new Tool(btn, nowPath, myContainerPanel, 0);
            //设置鼠标按下事件
            btn.AddHandler(Button.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler((sender, e) =>
            {
                bool isTopMost = this.Topmost;
                if (e.ClickCount == 1)
                {
                    this.Topmost = false;

                }

                Brush oldBrush = btn.Background;
                btn.Background = Brushes.Green;
                ////自写代码
                FileDDWatcher.Instance.StartWatcher(1);// 启动文件挪移监控,传入文件挪移的个数
                string dir = rootPath + @"UnityBox\DragToolPath\" + btn.Content.ToString();
                //myRichBox.AppendText("dragOutContent:" + btn.Content.ToString());

                string[] paths = new string[] { dir };



                #region 拖拽代码

                ListView lv = new ListView();
                string dataFormat = DataFormats.FileDrop;
                DataObject dataObject = new DataObject(dataFormat, paths);
                DragDropEffects dde = DragDrop.DoDragDrop(lv, dataObject, DragDropEffects.Copy);

                #endregion

                FileDDWatcher.Instance.StopWatcher();// 停止文件监控

                #region 拖拽成功之后获取目标的路径
                Console.WriteLine($"dropdrag status = {dde}");
                if (dde == DragDropEffects.Copy)// 拖拽成功
                {
                    string[] ls = FileDDWatcher.Instance.GetTempFilePath();// 获取拖拽之后的目录
                    if (ls != null && ls.Length > 0)
                    {
                        for (int i = 0; i < ls.Length; i++)
                        {
                            // myRichBox.AppendText($"Target catalogue {ls[i]}");

                        }
                    }
                    else
                    {
                        // myRichBox.AppendText("no file path copy");

                    }
                    btn.Background = oldBrush;
                    oneTool.useTime += 1;
                }
                this.Topmost = isTopMost;
                #endregion

                //dragOutContent = btn.Content.ToString();
                ////dragOutContent = (sender as Button).Name;
                //myRichBox.AppendText("\n" + dragOutContent + ":拖拽成功");

            }), true);
            //设置鼠标双击事件
            btn.AddHandler(Button.MouseDoubleClickEvent, new MouseButtonEventHandler((sender, e) =>
            {
                if (e.ChangedButton!=MouseButton.Left)
                {
                    return;
                }

                //myRichBox.AppendText("\n" + dragOutContent + ":拖拽结束");
                if (nowPath != "" && nowPath != null)
                {
                    // string filePath = buttonPath.Replace(buttonName, "");
                    if (System.IO.File.Exists(nowPath) || System.IO.Directory.Exists(nowPath))
                    {

                        System.Diagnostics.Process.Start("explorer.exe", nowPath);

                    }
                    else
                    {
                        //bool isTopMost = this.Topmost;
                        //this.Topmost = false;
                        MessageBox.Show(nowPath + ":文件已被转移或被删除");
                        //this.Topmost = isTopMost;
                    }
                }
                else
                {
                    //bool isTopMost = this.Topmost;
                    //this.Topmost = false;
                    MessageBox.Show("没有指定路径！！！");
                    //this.Topmost = isTopMost;
                    return;

                }
                oneTool.useTime += 1;

            }), true);
            //删除事件
            btn.MouseDown +=
                    (sender, e) =>
                    {

                        if (e.ChangedButton == MouseButton.Middle & e.ButtonState == MouseButtonState.Pressed)

                            if (Keyboard.IsKeyDown(Key.LeftCtrl))
                            {
                                //删除文件
                                try
                                {
                                    DeleteFolder(nowPath);

                                }
                                catch
                                {
                                    //bool isTopMost = this.Topmost;
                                    //this.Topmost = false;
                                    MessageBox.Show("删除失败，UnityBox\\Backup\\DeleteFiles存在同名文件,请先清理此文件。");
                                    //this.Topmost = isTopMost;
                                    return;
                                }

                                myAllDragTools.removeTool(btn.Content.ToString());
                                myContainerPanel.Children.Remove(btn);
                                //oneTool.ShowButton();
                                dragToolDf.SavePlayerData(myAllDragTools);
                            }
                            else
                            {
                                //bool isTopMost = this.Topmost;
                                //this.Topmost = false;
                                MessageBoxResult isGoToDelete = MessageBox.Show("是否真的要删除推拽素材：" + btn.Content.ToString() + "?", "警告", MessageBoxButton.YesNo, MessageBoxImage.Information);
                                //this.Topmost = isTopMost;
                                if (isGoToDelete == MessageBoxResult.Yes)
                                {
                                    //删除文件
                                    try
                                    {
                                        DeleteFolder(nowPath);

                                    }
                                    catch
                                    {
                                        //isTopMost = this.Topmost;
                                        //this.Topmost = false;
                                        MessageBox.Show("删除失败，UnityBox\\Backup\\DeleteFiles存在同名文件,请先清理此文件。");
                                        //this.Topmost = isTopMost;
                                        return;
                                    }

                                    myAllDragTools.removeTool(btn.Content.ToString());
                                    myContainerPanel.Children.Remove(btn);
                                    //oneTool.ShowButton();
                                    dragToolDf.SavePlayerData(myAllDragTools);
                                }
                            }


                    };
            //显示工具信息1
            btn.MouseEnter +=
                (sender, e) =>
                {
                    UnityToolWindow.Title = "拖拽素材：" + btn.Content.ToString() + "---使用次数" + myAllDragTools.allTools[btn.Content.ToString()].useTime + "次";
                };
            //显示工具信息2
            btn.MouseLeave +=
                (sender, e) =>
                {
                    UnityToolWindow.Title = winTitle;
                };
            if (myAllDragTools.allTools.ContainsKey(nowPath))
            {
                //myAllDragTools.allTools[nowPath] = oneTool;
                //bool isTopMost = this.Topmost;
                //this.Topmost = false;
                MessageBox.Show("新建拖拽素材失败，已有同名工具");
                //this.Topmost = isTopMost;
                return;

            }
            else
            {
                myAllDragTools.addTool(oneTool);
                DragTabs.SelectedItem = aimTab;

            }
            //oneTool.ShowButton();
            myContainerPanel.Children.Add(btn);
            ScrollViewer sv = (ScrollViewer)myContainerPanel.Parent;
            sv.ScrollToEnd();
            dragToolDf.SavePlayerData(myAllDragTools);

        }
        //函数----新加一个shortcutTool
        void MakeShortcutTool(string _fileName, string _path)
        {

            if (myAllShortCutTools.allTools.ContainsKey(_fileName))
            {
                //MessageBox.Show("新建快捷方式失败，已有同名工具");
                return;
            }

            Uri uri = new Uri(@"Resources\快捷导向.jpg", UriKind.Relative);
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(uri);
            Button btn = new Button
            {
                Name = "newButton",
                Content = _fileName,
                Width = 100,
                Height = 40,
                // Background= (System.Windows.Media.Brushes)("#FFFFE79E")
                Background = ib
            };

            //删除事件
            btn.MouseDown +=
                (sender, e) =>
                {
                    if (e.ChangedButton == MouseButton.Middle & e.ButtonState == MouseButtonState.Pressed)

                        if (Keyboard.IsKeyDown(Key.LeftCtrl))
                        {
                            myAllShortCutTools.removeTool(btn.Content.ToString());
                            this.ShortcutsPanel.Children.Remove(btn);
                            shortCutToolDf.SavePlayerData(myAllShortCutTools);
                        }
                        else
                        {
                            //bool isTopMost = this.Topmost;
                            //this.Topmost = false;
                            MessageBoxResult isGoToDelete = MessageBox.Show("是否真的要删除快捷方式：" + btn.Content.ToString() + "?", "警告", MessageBoxButton.YesNo, MessageBoxImage.Information);
                            //this.Topmost = isTopMost;
                            if (isGoToDelete == MessageBoxResult.Yes)
                            {
                                myAllShortCutTools.removeTool(btn.Content.ToString());
                                this.ShortcutsPanel.Children.Remove(btn);
                                shortCutToolDf.SavePlayerData(myAllShortCutTools);
                            }
                        }
                };

            //显示工具信息1
            btn.MouseEnter +=
                (sender, e) =>
                {
                    UnityToolWindow.Title = "快捷方式：" + btn.Content.ToString() + "---使用次数" + myAllShortCutTools.allTools[btn.Content.ToString()].useTime + "次";
                };
            //显示工具信息2
            btn.MouseLeave +=
                (sender, e) =>
                {
                    UnityToolWindow.Title = winTitle;
                };
            Tool oneTool = new Tool(btn, _path, ShortcutsPanel, 0);
            myAllShortCutTools.addTool(oneTool);
            ShortcutsPanel.Children.Add(btn);
            //oneTool.ShowButton();
            ScrollViewer sv = (ScrollViewer)ShortcutsPanel.Parent;
            sv.ScrollToEnd();
            shortCutToolDf.SavePlayerData(myAllShortCutTools);
            //.SavePlayerData(myAllShortCutTools);

        }
        //监测拖拽事件
        #endregion
        /// <summary>
        ///内拖拽功能
        ///*************************************************************************************************************************************
        /// <summary>




        /// <summary>
        ///静态函数**************静态函数*********静态函数****************静态函数***************静态函数************静态函数***********静态函数
        /// <summary>
        #region


        /// 删除文件夹（及文件夹下所有子文件夹和文件）
        public static void DeleteFolder(string directoryPath)
        {
            string deleteFilesPath = rootPath + @"UnityBox\Backup\DeleteFiles";
            if (Directory.Exists(deleteFilesPath) == false)//如果不存在就创建file文件夹{
                Directory.CreateDirectory(deleteFilesPath);

            string[] fileName = directoryPath.Split('\\');

            try

            {


                if (File.Exists(directoryPath))
                {
                    File.Move(directoryPath, deleteFilesPath + "\\" + fileName[fileName.Length - 1]);


                    // File.Delete(directoryPath);
                    return;
                }
                else
                {
                    Directory.Move(directoryPath, deleteFilesPath + "\\" + fileName[fileName.Length - 1]);


                    //foreach (string d in Directory.GetFileSystemEntries(directoryPath))
                    //{
                    //    if (File.Exists(d))
                    //    {
                    //        FileInfo fi = new FileInfo(d);
                    //        if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                    //            fi.Attributes = FileAttributes.Normal;
                    //        File.Delete(d);     //删除文件   
                    //    }
                    //    else
                    //        DeleteFolder(d);    //删除文件夹
                    //}
                    //Directory.Delete(directoryPath);    //删除空文件夹
                }
            }
            catch
            {

                MessageBox.Show(deleteFilesPath + "已存在同名删除文件，请先清理！");
            }

        }

        //复制文件或者文件夹
        public static void CopyDirectory(String sourcePath, String destinationPath)
        {

            if (File.Exists(sourcePath))
            {

                File.Copy(sourcePath, destinationPath, false);
                return;
            }

            DirectoryInfo info = new DirectoryInfo(sourcePath);
            Directory.CreateDirectory(destinationPath);

            foreach (FileSystemInfo fsi in info.GetFileSystemInfos())
            {
                String destName = System.IO.Path.Combine(destinationPath, fsi.Name);

                if (fsi is System.IO.FileInfo)          //如果是文件，复制文件
                    File.Copy(fsi.FullName, destName, false);
                else                                    //如果是文件夹，新建文件夹，递归
                {
                    Directory.CreateDirectory(destName);
                    CopyDirectory(fsi.FullName, destName);
                }
            }


        }


        //获取文件夹下的子目录所有路径,返回字典，键值对--》（文件名+完整路径）,并且排序

        public static Dictionary<string, string> GetAllDic(string _filePath)
        {


            DirectoryInfo d = new DirectoryInfo(_filePath);
            //实例化DirectoryInfo
            FileSystemInfo[] f = d.GetFileSystemInfos();

            Dictionary<string, string> myDicString = new Dictionary<string, string>();
            foreach (FileSystemInfo di in f)
            {

                myDicString.Add(di.Name, di.FullName);
                //myRichBox.AppendText("添加路径：" + di.FullName + "\n");

            }
            // myRichBox.AppendText("长度：" + myDicString.Count);


            myDicString = (from entry in myDicString
                           orderby entry.Key ascending
                           select entry).ToDictionary(pair => pair.Key, pair => pair.Value);

            return myDicString;
        }

        //复制到剪切板
        public static void CopyToClipboard(string[] files, bool cut)
        {
            if (files == null) return;
            IDataObject data = new DataObject(DataFormats.FileDrop, files);
            MemoryStream memo = new MemoryStream(4);
            byte[] bytes = new byte[] { (byte)(cut ? 2 : 5), 0, 0, 0 };
            memo.Write(bytes, 0, bytes.Length);
            data.SetData("PreferredDropEffect", memo);
            Clipboard.SetDataObject(data, false);
        }
        #endregion
        /// <summary>
        ///静态函数**************静态函数*********静态函数****************静态函数***************静态函数************静态函数***********静态函数
        /// <summary>





        #region

        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {


        }
        private void Window_DragOver(object sender, DragEventArgs e)
        {
            //var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            //if (HasDirectory(files))
            //{
            //    e.Effects = DragDropEffects.None;// 抛弃这一次的拖拽源
            //    e.Handled = true;
            //}
        }
        private void Unity百宝箱_PreviewDrop(object sender, DragEventArgs e)
        {

        }



        #endregion

        private void UnityToolWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            dragToolDf.SavePlayerData(myAllDragTools);
            shortCutToolDf.SavePlayerData(myAllShortCutTools);



            //MessageBoxResult result = MessageBox.Show("确定是退出吗？", "询问", MessageBoxButton.YesNo, MessageBoxImage.Question);

            ////关闭窗口
            //if (result == MessageBoxResult.Yes)
            //    e.Cancel = false;

            ////不关闭窗口
            //if (result == MessageBoxResult.No)
            //    e.Cancel = true;

        }


        private void DownLoadScroll_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Tips_Loaded(object sender, RoutedEventArgs e)
        {
            string myText = "";
            myText += "您好：这个模块本来是可以下载资源的地方，正在急速制作中，请期待2.0版本。qq群：885782989\n使用说明：";
            myText += "\n**********************************************************************************************\n视频使用说明，请在b站搜索我的账号\"知行合一unity\"，视频解说在我的作品内。";
            myText += "\n--1.新增：鼠标可以拖拽任何文件到\"快捷方式\"模块，或者\"拖拽素材\"模块，新增按钮。\n";
            myText += "区别：\"快捷方式\"模块消耗的空间可以忽略不计，鼠标左键单击按钮即可打开引用的文件,不支持向外拖拽。";
            myText += "\n而\"拖拽素材\"模块，将拷贝一份文件存储在UnityBox\\DragToolPath文件夹中，支持双向拖拽，直接打开文件需要鼠标左键双击按钮。";
            myText += "\n--2.删除：鼠标中键点击按钮删除\ntip1:按住ctrl，执行删除操作不会弹窗 \ntip2：删除素材可在UnityBox\\Backup\\DeleteFiles中找到，是一种安全机制";
            myText += "\n--3.快捷使用：（1）.鼠标右键切换常用窗口。（2）.键盘：crtl+\"~键\"或者123456789键组合可显示程序并跳转对应窗口， ctrl+Q最小化隐藏窗口。";
            myText += "\n--4.其他：\n（1）.把文件直接拖入\"拖拽素材\"模块将自动分类，不需要特别放在哪个子模块。\n（2）.每次重开程序，会按照使用频率进行按钮排序，(名字顺序-》使用次数)\n（3）.\"拖拽素材\"模块快速增加按钮方法是,";
            myText += "关闭程序，只要把要用的资源复制黏贴到UnityBox\\DragToolPath文件夹内，下次打开就会自动更新，并且分类。\n（4）.如果您硬盘吃紧，定期清理UnityBox\\Backup\\DeleteFiles，删除的文件没有彻底删除，在里面。";
            myText += "\n（5）.如需要了解UnityBox文件夹的内容含义，请观看使用说明视频，你可以学到如何还原备份等更多操作。\n（6）.通过直接删除（慎用）或转移UnityBox文件夹达到程序重置";
            myText += "\n**********************************************************************************************\n关于作者：从来没用过wpf，我特么做这个做了2周，还不如winform实在，我是那跟筋搭错才考虑尝试wpf程序，加重了我的自卑，自闭，抑郁，失眠。\n还是希望大家能喜欢，还有个模块尽量完成，请期待。";
            tips.Text = myText;

            /*
             您好：
\n本模块涉及到服务器 数据库，正在急速制作中，请期待2.0版本。
\nqq群：885782989
\n使用说明：
\n\n1. 新增：鼠标可以拖拽任何文件到-》”快捷方式“模块，或者”拖拽素材“模块，新增按钮。
	\n区别：快捷方式模块消耗的空间可以忽略不计，单击鼠标左键即可打开引用的文件,不支持向外拖拽。
	          \n而拖拽素材模块，将拷贝一份文件存储在UnityBox\DragToolPath文件夹中，支持双向拖拽。
\n\n2.删除：鼠标中键点击按钮删除，按住ctrl，鼠标中键盘 
             
             */
        }


        //初始化翻译软件





        //Translate.LanguageServiceClient client = new Translate.LanguageServiceClient();
        private void UnityToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WebB_LoadCompleted(object sender, NavigationEventArgs e)
        {
            // mshtml.HTMLDocument dom = (mshtml.HTMLDocument)(webB).Document; //定义HTML
            //dom.documentElement.style.overflow = "hidden";    //隐藏浏览器的滚动条 
            //dom.body.setAttribute("scroll", "no");            //禁用浏览器的滚动条
        }

        private void MyTabControl_MouseRightButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (myTabControl.SelectedIndex != 1)
            {
                myTabControl.SelectedIndex = 1;

            }
            else
            {
                int nextTabIndex = DragTabs.SelectedIndex + 1;
                if (nextTabIndex == DragTabs.Items.Count)
                {
                    DragTabs.SelectedIndex = 0;
                    myTabControl.SelectedIndex = 0;
                }
                else
                {
                    DragTabs.SelectedIndex = nextTabIndex;
                }
            }
        }
    }


}

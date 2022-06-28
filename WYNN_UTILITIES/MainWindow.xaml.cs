using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Media.Animation;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Win32;
using KrnlAPI;
using System.Windows.Forms;
using ScintillaNET;

namespace WYNNUTILITIES
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KrnlApi krnlAPI = new KrnlApi();

        public MainWindow()
        {
            InitializeComponent();
            krnlAPI.Initialize();
        }

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ExitApplication_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MultiInstances_Clicked(object sender, RoutedEventArgs e)
        {
            new Mutex(true, "ROBLOX_singletonMutex");
            System.Windows.MessageBox.Show("Multi-roblox is on!", "Success");
        }

        private void TOPMOST_CheckChange(object sender, RoutedEventArgs e)
        {
            if (TOPMOST.IsChecked == true)
            {
                this.Topmost = true;
            }
            if (TOPMOST.IsChecked != true)
            {
                this.Topmost = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            krnlAPI.Execute(texteditor.Text); //execute
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            krnlAPI.Inject(); //inject
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void setfpscap_Click(object sender, RoutedEventArgs e)
        {
            int fps = int.Parse(fpscap.Text);
            krnlAPI.SetFrameRate(fps);
        }

        private void getkey_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://krnl.place/getkey.php");
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmation = System.Windows.MessageBox.Show("Current script will be replaced, are you sure you want to continue?\x0a (Note: Adding tabs in future versions)", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.Yes)
            {
                Microsoft.Win32.OpenFileDialog scriptfile = new Microsoft.Win32.OpenFileDialog();
                scriptfile.Filter = "Text or LUA files (*.txt, *.lua)|*.txt;*.lua";
                if (scriptfile.ShowDialog() == true)
                    texteditor.Text = File.ReadAllText(scriptfile.FileName);
            }
            else if (confirmation == MessageBoxResult.No)
            {
                return;
            }
        }

        ScintillaNET.Scintilla scintilla;
        private void scinwinform_Loaded(object sender, RoutedEventArgs e)
        {
            scintilla = new ScintillaNET.Scintilla();
            texteditor.Text = "-- made by eforcarl";
            texteditor.Margins[0].Type = MarginType.RightText;
            texteditor.Margins[0].Width = 25;

            texteditor.SetSelectionBackColor(true, System.Drawing.ColorTranslator.FromHtml("#8b8c8c"));

            texteditor.StyleResetDefault();
            texteditor.Styles[ScintillaNET.Style.Default].BackColor = System.Drawing.ColorTranslator.FromHtml("#212121");
            texteditor.Styles[ScintillaNET.Style.Default].ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            texteditor.Styles[ScintillaNET.Style.Default].Font = "Consolas";
            texteditor.Styles[ScintillaNET.Style.Default].Size = 12;
            texteditor.StyleClearAll();

            texteditor.Styles[ScintillaNET.Style.LineNumber].BackColor = System.Drawing.ColorTranslator.FromHtml("#3d3d3d");
            texteditor.Styles[ScintillaNET.Style.LineNumber].ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            texteditor.Styles[ScintillaNET.Style.IndentGuide].ForeColor = System.Drawing.ColorTranslator.FromHtml("#3d3d3d");
            texteditor.Styles[ScintillaNET.Style.IndentGuide].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

            var nums = texteditor.Margins[0];
            nums.Width = 30;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;


            // texteditor.Styles[ScintillaNET.Style.Lua.
            texteditor.Styles[ScintillaNET.Style.Lua.CommentLine].ForeColor = System.Drawing.Color.Green;
            texteditor.Styles[ScintillaNET.Style.Lua.Comment].ForeColor = System.Drawing.Color.Green;
            texteditor.Styles[ScintillaNET.Style.Lua.String].ForeColor = System.Drawing.Color.Orange;
            texteditor.Styles[ScintillaNET.Style.Lua.Number].ForeColor = System.Drawing.Color.LightBlue;
            texteditor.Styles[ScintillaNET.Style.Lua.Character].ForeColor = System.Drawing.Color.LightGreen;
            texteditor.Styles[ScintillaNET.Style.Lua.LiteralString].ForeColor = System.Drawing.Color.LightGreen;
            texteditor.Styles[ScintillaNET.Style.Lua.StringEol].ForeColor = System.Drawing.Color.Pink;
            texteditor.Styles[ScintillaNET.Style.Lua.Preprocessor].ForeColor = System.Drawing.Color.Teal;
            texteditor.Styles[ScintillaNET.Style.Lua.Operator].ForeColor = System.Drawing.Color.LightSeaGreen;
            texteditor.Styles[ScintillaNET.Style.Lua.Word].ForeColor = System.Drawing.Color.Blue;
            texteditor.Styles[ScintillaNET.Style.Lua.Word2].ForeColor = System.Drawing.Color.BlueViolet;
            texteditor.Styles[ScintillaNET.Style.Lua.Word3].ForeColor = System.Drawing.Color.DarkSlateBlue;
            texteditor.Styles[ScintillaNET.Style.Lua.Word4].ForeColor = System.Drawing.Color.DarkSlateBlue;

            texteditor.Lexer = Lexer.Lua;

            // Keywords
            scintilla.SetKeywords(0, "and break do else elseif end for function if in local nil not or repeat return then until while" + " false true" + " goto");
            // Basic Functions
            scintilla.SetKeywords(1, "assert collectgarbage dofile error _G getmetatable ipairs loadfile next pairs pcall print rawequal rawget rawset setmetatable tonumber tostring type _VERSION xpcall string table math coroutine io os debug" + " getfenv gcinfo load loadlib loadstring require select setfenv unpack _LOADED LUA_PATH _REQUIREDNAME package rawlen package bit32 utf8 _ENV");
            // String Manipulation & Mathematical
            scintilla.SetKeywords(2, "string.byte string.char string.dump string.find string.format string.gsub string.len string.lower string.rep string.sub string.upper table.concat table.insert table.remove table.sort math.abs math.acos math.asin math.atan math.atan2 math.ceil math.cos math.deg math.exp math.floor math.frexp math.ldexp math.log math.max math.min math.pi math.pow math.rad math.random math.randomseed math.sin math.sqrt math.tan" + " string.gfind string.gmatch string.match string.reverse string.pack string.packsize string.unpack table.foreach table.foreachi table.getn table.setn table.maxn table.pack table.unpack table.move math.cosh math.fmod math.huge math.log10 math.modf math.mod math.sinh math.tanh math.maxinteger math.mininteger math.tointeger math.type math.ult" + " bit32.arshift bit32.band bit32.bnot bit32.bor bit32.btest bit32.bxor bit32.extract bit32.replace bit32.lrotate bit32.lshift bit32.rrotate bit32.rshift" + " utf8.char utf8.charpattern utf8.codes utf8.codepoint utf8.len utf8.offset");
            // Input and Output Facilities and System Facilities
            scintilla.SetKeywords(3, "coroutine.create coroutine.resume coroutine.status coroutine.wrap coroutine.yield io.close io.flush io.input io.lines io.open io.output io.read io.tmpfile io.type io.write io.stdin io.stdout io.stderr os.clock os.date os.difftime os.execute os.exit os.getenv os.remove os.rename os.setlocale os.time os.tmpname" + " coroutine.isyieldable coroutine.running io.popen module package.loaders package.seeall package.config package.searchers package.searchpath" + " require package.cpath package.loaded package.loadlib package.path package.preload");


        }

        private void texteditor_TextChanged(object sender, EventArgs e)
        {

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmation2 = System.Windows.MessageBox.Show("Are you sure? Current script will be cleared", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation2 == MessageBoxResult.Yes)
            {
                texteditor.Text = "";
            }
            else if (confirmation2 == MessageBoxResult.No)
            {
                return;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using DiscordRPC;
using DiscordRPC.Logging;
using DiscordRPC.Message;
using System.Windows.Forms;
using System.IO;

namespace NoForActa2RPC {
    class Program {
        public DiscordRpcClient client;
        private NotifyIcon icon;
        private ContextMenu contextMenu;
        private MenuItem menuItem;
        private MenuItem langSwitch;
        private MenuItem langpl;
        private MenuItem langen;
        private System.ComponentModel.IContainer components;

        static void Main(string[] args) {
            Program pg = new Program(); 
            Application.Run();
        }
        Program() {
            CreateNotifyicon();
        }
        private void CreateNotifyicon() {
            //setPresence("en");
            this.components = new System.ComponentModel.Container();
            this.contextMenu = new ContextMenu();
            this.menuItem = new MenuItem();
            this.menuItem.Index = 1;
            this.menuItem.Text = "Exit";
            this.menuItem.Click += new System.EventHandler(this.mClick);

            this.langSwitch = new MenuItem();
            this.langSwitch.Index = 0;
            this.langSwitch.Text = "Language";
            this.contextMenu.MenuItems.AddRange(
                new MenuItem[] { this.langSwitch, this.menuItem });

            this.langpl = new MenuItem() {
                Index = 3,
                Text = "Polski",
            };
            
            this.langen = new MenuItem() {
                Index = 2,
                Text = "English"
            };

            this.langpl.Click += new System.EventHandler(this.switchPolski);
            this.langen.Click += new System.EventHandler(this.switchEnglish);
            this.langSwitch.MenuItems.AddRange(
                new MenuItem[] { this.langpl, this.langen });
            this.icon = new NotifyIcon(this.components);
            icon.Icon = new Icon("ico.ico");
            icon.ContextMenu = this.contextMenu;

            icon.Text = "NoForActa2.0 RPC";
            icon.Visible = true;

            client = new DiscordRpcClient("462582726554288168", true);
            client.Initialize();
            setPresence("en");
        }

        private void mClick(object Sender, EventArgs e) {
            Application.Exit();
        }
        private async void setPresence(string language) {
            RichPresence polski = new RichPresence() {
                Details = "Nie dla ACTA 2!",
                State = "saveyourinternet.eu/pl",
                Assets = new Assets() {
                    LargeImageKey = "stop",
                    LargeImageText = "antyart13.pl",
                    SmallImageKey = "eu",
                    SmallImageText = "eu"
                }
            };
            RichPresence english = new RichPresence() {
                Details = "No for Art 13!",
                State = "saveyourinternet.eu",
                Assets = new Assets() {
                    LargeImageKey = "stop",
                    LargeImageText = "DeleteArt13",
                    SmallImageKey = "eu",
                    SmallImageText = "eu"
                }
            };
            
            
            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };
            Console.WriteLine("and the language is" + language);
            if (language == "pl") client.SetPresence(polski);
            else client.SetPresence(english);
        }
        private void switchPolski(object Sender, EventArgs e) {
            setPresence("pl");
            this.menuItem.Text = "Wyjdź";
            this.langSwitch.Text = "Język";
        }
        private void switchEnglish(object Sender, EventArgs e) {
            setPresence("en");
            this.menuItem.Text = "Exit";
            this.langSwitch.Text = "Language";
        }
    }
}

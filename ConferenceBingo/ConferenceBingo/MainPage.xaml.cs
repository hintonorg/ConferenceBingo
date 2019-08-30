using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ConferenceBingo
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        //string[,] aBingoBoard;
        List<BingoClass> BingoList;

        public MainPage()
        {
            //int count = 0;

            InitializeComponent();

            //*************************

            grdBingo.BackgroundColor = System.Drawing.Color.Blue;

            Initboard();
        }

        #region Functions
        private void Initboard()
        {
            int iCnt = 0;


            //playAgainButton.IsVisible = false;
            //XImage.IsVisible = false;

            /*
            aBingoBoard = new string[5, 5] {{"N","N","N","N","N"},
                                    {"N","N","N","N","N"},
                                    {"N","N","N","N","N"},
                                    {"N","N","N","N","N"},
                                    {"N","N","N","N","N"} };

            */

            BingoList = new List<BingoClass>();

            for (int i = 0; i < 27; i++)
            {
                BingoClass bingo = new BingoClass();
                bingo.ClickedOn = "N";
                bingo.image = new Image();
                bingo.image.BackgroundColor = System.Drawing.Color.White;
                bingo.image.ClassId = i.ToString();
                bingo.Id = i;
                //bingo.image.HorizontalOptions = LayoutOptions.FillAndExpand;
                //bingo.image.VerticalOptions = LayoutOptions.FillAndExpand;
                bingo.image.Aspect = Aspect.AspectFit;

                //****************

                if (i == 25)
                {
                    bingo.image.Source = ImageSource.FromFile("XT.png");
                }
                else if(i == 26)
                {
                    bingo.image.Source = ImageSource.FromFile("Line.png");
                }
                else
                { 
                    bingo.image.Source = ImageSource.FromFile("img" + i.ToString() + ".png");
                    //bingo.image.Source = ImageSource.FromFile("img10.jpg");
                }

                BingoList.Add(bingo);
            }

            //*********************

            /*
            List<ImageSource> ImgList = new List<ImageSource>();
            ImgList.Add(ImageSource.FromFile("XT.png"));
            ImgList.Add(ImageSource.FromFile("Line.png"));
            */
            //*********************
            var tapGestureRecognizer = new TapGestureRecognizer();
            //tapGestureRecognizer.Tapped += (s, e) =>

            tapGestureRecognizer.Tapped += (sender, e) =>
            {
                // handle the tap
                MyImage_Clicked(sender, e);
            };

            //*********************

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Image MyImage = new Image();
                    MyImage.BackgroundColor = System.Drawing.Color.White;

                    MyImage.ClassId = iCnt.ToString();
                    MyImage.Aspect = Aspect.AspectFit;
                    MyImage.GestureRecognizers.Add(tapGestureRecognizer);

                    MyImage.Source = BingoList[iCnt].image.Source;

                    Grid.SetColumn(MyImage, j);
                    Grid.SetRow(MyImage, i);

                    //****************
                    grdBingoOverlay.Children.Add(MyImage);

                    Image MyImageOverlay = new Image();
                    MyImageOverlay.Source = "XT.png";

                    Grid.SetColumn(MyImageOverlay, j);
                    Grid.SetRow(MyImageOverlay, i);


                    grdBingoOverlay.Children.Add(MyImageOverlay);

                    //Grid.SetColumn(MyImage, j);
                    //Grid.SetRow(MyImage, i);

                    //****************
                    //grdBingoOverlay.Children.Add(MyImage);

                    grdBingo.Children.Add(grdBingoOverlay);


                    iCnt++;
                }
            }
        }

        private void Resetboard()
        {
            InitializeComponent();

            grdBingo.BackgroundColor = System.Drawing.Color.Blue;

            Initboard();
        }

        /*
        private void DisplayPlayAgainButton(double x, double y)
        {
            XImage.Scale = 0;
            XImage.IsVisible = true;
            XImage.IsEnabled = true;

            // (See above for rationale)
            //double playAgainButtonWidth = playAgainButton.Measure(Double.PositiveInfinity, Double.PositiveInfinity).Request.Width;
            //double playAgainButtonWidth = playAgainButton.Measure(x, y).Request.Width;    //DEBUG

            //double maxScale = board.Width / playAgainButtonWidth;
            //double maxScale = 50 / playAgainButtonWidth;  //DEBUG
            double maxScale = 10;

            //playAgainButton.ScaleTo(maxScale, 1000, Easing.SpringOut);
            XImage.ScaleTo(maxScale, 1000, Easing.SpringOut);    //DEBUG


            //Add the location image and place on the view
        }
        */

        //async Task DisplayPlayAgainButton()
        /*
        private void DisplayPlayAgainButton()
        {
            playAgainButton.Scale = 0;
            playAgainButton.IsVisible = true;
            playAgainButton.IsEnabled = true;

            // (See above for rationale)
            double playAgainButtonWidth = playAgainButton.Measure(Double.PositiveInfinity, Double.PositiveInfinity).Request.Width;

            //double maxScale = board.Width / playAgainButtonWidth;
            double maxScale = 200 / playAgainButtonWidth;
            playAgainButton.ScaleTo(maxScale, 1000, Easing.SpringOut);
        }
        */
        #endregion Functions

        #region Events
        private void MyImage_Clicked(object sender, EventArgs e)
        {
            int Id;
            Image img = (Image)sender;
            Image MyImage;

            //aBingoBoard[Convert.ToInt32(img.ClassId.Substring(0, 1)), Convert.ToInt32(img.ClassId.Substring(1, 1))] = "Y";
            Id = BingoList[Convert.ToInt32(img.ClassId)].Id;

            if(BingoList[Id].ClickedOn == "N")
            { 
                BingoList[Id].ClickedOn = "Y";
                //img.Source = BingoList[Id].image.Source;    //XT image
                //img.Source = BingoList[25].image.Source;    //XT image
                //DisplayPlayAgainButton(img.X, img.Y);

                img.Source = "Line.png";    //XT image
                img.Source = "XT.png";
            }
            else
            {
                BingoList[Id].ClickedOn = "N";
                img.Source = BingoList[Id].image.Source;
            }

            //******************
            //Horizontal lines

            for (int i = 0; i < 5; i++)
            {
                if ((BingoList[0].ClickedOn == "Y") && (BingoList[1].ClickedOn == "Y") && (BingoList[2].ClickedOn == "Y") && (BingoList[3].ClickedOn == "Y") && (BingoList[4].ClickedOn == "Y"))
                {
                    MyImage = (Image)grdBingo.Children[i];
                    MyImage.Source = BingoList[26].image.Source;    //Line

                    //DisplayAlert("Congratulations", "You won the game", "OK");
                }
            }

            for (int i = 5; i < 10; i++)
            {
                if ((BingoList[5].ClickedOn == "Y") && (BingoList[6].ClickedOn == "Y") && (BingoList[7].ClickedOn == "Y") && (BingoList[8].ClickedOn == "Y") && (BingoList[9].ClickedOn == "Y"))
                {
                    MyImage = (Image)grdBingo.Children[i];
                    MyImage.Source = BingoList[26].image.Source;    //Line

                    //DisplayAlert("Congratulations", "You won the game", "OK");
                }
            }

            for (int i = 10; i < 15; i++)
            {
                if ((BingoList[10].ClickedOn == "Y") && (BingoList[11].ClickedOn == "Y") && (BingoList[12].ClickedOn == "Y") && (BingoList[13].ClickedOn == "Y") && (BingoList[14].ClickedOn == "Y"))
                {
                    MyImage = (Image)grdBingo.Children[i];
                    MyImage.Source = BingoList[26].image.Source;    //Line

                    //DisplayAlert("Congratulations", "You won the game", "OK");
                }
            }

            for (int i = 15; i < 20; i++)
            {
                if ((BingoList[15].ClickedOn == "Y") && (BingoList[16].ClickedOn == "Y") && (BingoList[17].ClickedOn == "Y") && (BingoList[18].ClickedOn == "Y") && (BingoList[19].ClickedOn == "Y"))
                {
                    MyImage = (Image)grdBingo.Children[i];
                    MyImage.Source = BingoList[26].image.Source;    //Line

                    //DisplayAlert("Congratulations", "You won the game", "OK");
                }
            }

            for (int i = 20; i < 25; i++)
            {
                if ((BingoList[20].ClickedOn == "Y") && (BingoList[21].ClickedOn == "Y") && (BingoList[22].ClickedOn == "Y") && (BingoList[23].ClickedOn == "Y") && (BingoList[24].ClickedOn == "Y"))
                {
                    MyImage = (Image)grdBingo.Children[i];
                    MyImage.Source = BingoList[26].image.Source;    //Line

                    //DisplayAlert("Congratulations", "You won the game", "OK");
                }
            }

            //Vertical lines
            for (int i = 0; i < 21; i += 5)
            {
                if ((BingoList[0].ClickedOn == "Y") && (BingoList[5].ClickedOn == "Y") && (BingoList[10].ClickedOn == "Y") && (BingoList[15].ClickedOn == "Y") && (BingoList[20].ClickedOn == "Y"))
                {
                    MyImage = (Image)grdBingo.Children[i];
                    MyImage.Source = ImageSource.FromFile("Line.png");
                }
            }

            for (int i = 1; i < 22; i += 5)
            {
                if ((BingoList[1].ClickedOn == "Y") && (BingoList[6].ClickedOn == "Y") && (BingoList[11].ClickedOn == "Y") && (BingoList[16].ClickedOn == "Y") && (BingoList[21].ClickedOn == "Y"))
                {
                    MyImage = (Image)grdBingo.Children[i];
                    MyImage.Source = ImageSource.FromFile("Line.png");
                }
            }

            for (int i = 2; i < 23; i += 5)
            {
                if ((BingoList[2].ClickedOn == "Y") && (BingoList[7].ClickedOn == "Y") && (BingoList[12].ClickedOn == "Y") && (BingoList[17].ClickedOn == "Y") && (BingoList[22].ClickedOn == "Y"))
                {
                    MyImage = (Image)grdBingo.Children[i];
                    MyImage.Source = ImageSource.FromFile("Line.png");
                }
            }

            for (int i = 3; i < 24; i += 5)
            {
                if ((BingoList[3].ClickedOn == "Y") && (BingoList[8].ClickedOn == "Y") && (BingoList[13].ClickedOn == "Y") && (BingoList[18].ClickedOn == "Y") && (BingoList[23].ClickedOn == "Y"))
                {
                    MyImage = (Image)grdBingo.Children[i];
                    MyImage.Source = ImageSource.FromFile("Line.png");
                }
            }

            for (int i = 4; i < 25; i += 5)
            {
                if ((BingoList[4].ClickedOn == "Y") && (BingoList[9].ClickedOn == "Y") && (BingoList[14].ClickedOn == "Y") && (BingoList[19].ClickedOn == "Y") && (BingoList[24].ClickedOn == "Y"))
                {
                    MyImage = (Image)grdBingo.Children[i];
                    MyImage.Source = ImageSource.FromFile("Line.png");
                }
            }

            /*
            for (int i = 0; i < 5; i++)
            {
                if ((aBingoBoard[i, 0] == "Y") && (aBingoBoard[i, 1] == "Y") && (aBingoBoard[i, 2] == "Y") && (aBingoBoard[i, 3] == "Y") && (aBingoBoard[i, 4] == "Y"))
                {
                    for (int j = (i * 5); j < ((i * 5) + 5); j++)
                    {
                        MyImage = (Image)grdBingo.Children[j];
                        //MyImage = ImageSource.FromFile("Line.png");
                        MyImage.Source = ImageSource.FromFile("Line.png");
                    }
                }
            }
            */
            /*
            //Vertical lines
            for (int i = 0; i < 5; i++)
            {
                if ((aBingoBoard[0, i] == "Y") && (aBingoBoard[1, i] == "Y") && (aBingoBoard[2, i] == "Y") && (aBingoBoard[3, i] == "Y") && (aBingoBoard[4, i] == "Y"))
                {
                    for (int j = i; j < (i + 21); j += 5)
                    {
                        MyImage = (Image)grdBingo.Children[j];
                        MyImage.Source = ImageSource.FromFile("Line.png");
                    }
                }
            }

            */

            //Diagonal from top to right
            if ((BingoList[0].ClickedOn == "Y") && (BingoList[6].ClickedOn == "Y") && (BingoList[12].ClickedOn == "Y") && (BingoList[18].ClickedOn == "Y") && (BingoList[24].ClickedOn == "Y"))
            {
                for (int j = 0; j < 25; j += 6)
                {
                    MyImage = (Image)grdBingo.Children[j];
                    MyImage.Source = ImageSource.FromFile("Line.png");
                }
            }

            //Diagonal from top to left
            if ((BingoList[4].ClickedOn == "Y") && (BingoList[8].ClickedOn == "Y") && (BingoList[12].ClickedOn == "Y") && (BingoList[16].ClickedOn == "Y") && (BingoList[20].ClickedOn == "Y"))
            {
                for (int j = 4; j < 21; j += 4)
                {
                    MyImage = (Image)grdBingo.Children[j];
                    MyImage.Source = ImageSource.FromFile("Line.png");
                }
            }

        }

        /*
        private void MyImage_Clicked(object sender, EventArgs e)
        {
            Image img = (Image)sender;
            Image MyImage;

            aBingoBoard[Convert.ToInt32(img.ClassId.Substring(0, 1)), Convert.ToInt32(img.ClassId.Substring(1, 1))] = "Y";

            img.Source = ImageSource.FromFile("XT.png");

            //******************
            //Horizontal lines
            for (int i = 0; i < 5; i++)
            {
                if ((aBingoBoard[i, 0] == "Y") && (aBingoBoard[i, 1] == "Y") && (aBingoBoard[i, 2] == "Y") && (aBingoBoard[i, 3] == "Y") && (aBingoBoard[i, 4] == "Y"))
                {
                    for (int j = (i * 5); j < ((i * 5) + 5); j++)
                    {
                        MyImage = (Image)grdBingo.Children[j];
                        //MyImage = ImageSource.FromFile("Line.png");
                        MyImage.Source = ImageSource.FromFile("Line.png");
                    }
                }
            }

            //Vertical lines
            for (int i = 0; i < 5; i++)
            {
                if ((aBingoBoard[0, i] == "Y") && (aBingoBoard[1, i] == "Y") && (aBingoBoard[2, i] == "Y") && (aBingoBoard[3, i] == "Y") && (aBingoBoard[4, i] == "Y"))
                {
                    for (int j = i; j < (i + 21); j += 5)
                    {
                        MyImage = (Image)grdBingo.Children[j];
                        MyImage.Source = ImageSource.FromFile("Line.png");
                    }
                }
            }


            //Diagonal from top to right
            if ((aBingoBoard[0, 0] == "Y") && (aBingoBoard[1, 1] == "Y") && (aBingoBoard[2, 2] == "Y") && (aBingoBoard[3, 3] == "Y") && (aBingoBoard[4, 4] == "Y"))
            {
                for (int j = 0; j < 25; j += 6)
                {
                    MyImage = (Image)grdBingo.Children[j];
                    MyImage.Source = ImageSource.FromFile("Line.png");
                }
            }

            //Diagonal from top to left
            if ((aBingoBoard[0, 4] == "Y") && (aBingoBoard[1, 3] == "Y") && (aBingoBoard[2, 2] == "Y") && (aBingoBoard[3, 1] == "Y") && (aBingoBoard[4, 0] == "Y"))
            {
                for (int j = 4; j < 21; j += 4)
                {
                    MyImage = (Image)grdBingo.Children[j];
                    MyImage.Source = ImageSource.FromFile("Line.png");
                }
            }
        }
        */

        /*
        private void Button_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Button MyButton;

            aBingoBoard[Convert.ToInt32(btn.ClassId.Substring(0, 1)), Convert.ToInt32(btn.ClassId.Substring(1, 1))] = "Y";

            //count++;

            //string s = btn.Id.ToString();
            //string s1 = btn.Text;

            //((Button)sender).Text = $"You clicked {count} times";
            //btn11.Text = $"Testing {count} times";

            //((Button)sender).ImageSource = ImageSource.FromFile("XT.png");

            btn.ImageSource = ImageSource.FromFile("XT.png");

            //******************
            //Horizontal lines
            for (int i = 0; i < 5; i++)
            {
                if ((aBingoBoard[i, 0] == "Y") && (aBingoBoard[i, 1] == "Y") && (aBingoBoard[i, 2] == "Y") && (aBingoBoard[i, 3] == "Y") && (aBingoBoard[i, 4] == "Y"))
                {
                    for (int j = (i * 5); j < ((i * 5) + 5); j++)
                    {
                        MyButton = (Button)grdBingo.Children[j];
                        //MyButton.ImageSource = ImageSource.FromFile("Line.png");
                        MyButton.ImageSource = ImageSource.FromFile("XT.png");
                    }
                }
            }

            //Vertical lines
            for (int i = 0; i < 5; i++)
            {
                if ((aBingoBoard[0, i] == "Y") && (aBingoBoard[1, i] == "Y") && (aBingoBoard[2, i] == "Y") && (aBingoBoard[3, i] == "Y") && (aBingoBoard[4, i] == "Y"))
                {
                    for (int j = i; j < (i + 21); j+=5)
                    {
                        MyButton = (Button)grdBingo.Children[j];
                        MyButton.ImageSource = ImageSource.FromFile("XT.png");
                    }
                }
            }


            //Diagonal from top to right
            if ((aBingoBoard[0, 0] == "Y") && (aBingoBoard[1, 1] == "Y") && (aBingoBoard[2, 2] == "Y") && (aBingoBoard[3, 3] == "Y") && (aBingoBoard[4, 4] == "Y"))
            {
                for (int j = 0; j < 25; j+=6)
                {
                    MyButton = (Button)grdBingo.Children[j];
                    MyButton.ImageSource = ImageSource.FromFile("Line.png");
                }
            }

            //Diagonal from top to left
            if ((aBingoBoard[0, 4] == "Y") && (aBingoBoard[1, 3] == "Y") && (aBingoBoard[2, 2] == "Y") && (aBingoBoard[3, 1] == "Y") && (aBingoBoard[4, 0] == "Y"))
            {
                for (int j = 4; j < 21; j += 4)
                {
                    MyButton = (Button)grdBingo.Children[j];
                    MyButton.ImageSource = ImageSource.FromFile("Line.png");
                }
            }
        }
        */

        private void btnNewGame_Clicked(object sender, EventArgs e)
        {
            Resetboard();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }
        #endregion Events
    }
}

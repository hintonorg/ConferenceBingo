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
        List<BingoClass> BingoList;
        List<int> rList;
        bool bPlayBlackout = false;
        bool bIsBlackout;

        public MainPage()
        {
            //int count = 0;

            InitializeComponent();
            this.BindingContext = new MainPageViewModel();
            

            //*************************

            grdBingo.BackgroundColor = System.Drawing.Color.Black;

            Initboard();
        }

        #region Functions
        private void Initboard()
        {
            int iCnt = 0;

            if (BingoList is null)
            {
                BingoList = new List<BingoClass>();
                rList = new List<int>();
            }
            else
            {
                BingoList.Clear();
                rList.Clear();
            }            

            rList.Add(12);      //Add the middle number and always have it in the middle

            bIsBlackout = false;
            bPlayBlackout = false;

            for (int i = 0; i < 26; i++)
            {
                BingoClass bingo = new BingoClass();
                bingo.ClickedOn = "N";
                bingo.BlackoutClickedOn = "N";
                bingo.image = new Image();
                bingo.image.BackgroundColor = System.Drawing.Color.White;
                bingo.image.ClassId = i.ToString();
                bingo.Id = i;
                bingo.image.Aspect = Aspect.AspectFit;

                //****************

                if (i == 25)
                {
                    bingo.image.Source = ImageSource.FromFile("XT.png");
                }
                /*
                else if(i == 26)
                {
                    bingo.image.Source = ImageSource.FromFile("Line.png");
                }
                */
                else if (i == 12)   //Keep a free space in the middle
                {
                    bingo.image.Source = ImageSource.FromFile("img12.png");
                }
                else
                {
                    bingo.image.Source = ImageSource.FromFile("img" + GetRandomNum().ToString() + ".png");
                    //bingo.image.Source = ImageSource.FromFile("img10.jpg");
                }

                BingoList.Add(bingo);
            }

            //*********************

            var tapGestureRecognizer = new TapGestureRecognizer();

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

                    grdBingo.Children.Add(MyImage);

                    //****************
                    /*
                    grdBingoOverlay.Children.Add(MyImage);

                    Image MyImageOverlay = new Image();
                    MyImageOverlay.Source = "XT.png";

                    Grid.SetColumn(MyImageOverlay, j);
                    Grid.SetRow(MyImageOverlay, i);


                    grdBingoOverlay.Children.Add(MyImageOverlay);

                    grdBingo.Children.Add(grdBingoOverlay);
                    */

                    iCnt++;
                }
            }
        }

        private int GetRandomNum()
        {
            int i = 0, rInt = 0;

            while (true)
            {
                Random rdm = new Random();
                rInt = rdm.Next(0, 25);

                i++;

                if (!rList.Exists(x => x == rInt))
                { 
                    rList.Add(rInt);
                    break;
                }
            }

            return rInt;
        }

        private void Resetboard()
        {
            this.BindingContext = new MainPageViewModel();

            grdBingo.BackgroundColor = System.Drawing.Color.Black;

            Initboard();
        }

        private int GetColumn(int Id)
        {
            int iCol = 0;

            if ((Id == 0) || (Id == 5) || (Id == 10) || (Id == 15) || (Id == 20))
                iCol = 0;
            if ((Id == 1) || (Id == 6) || (Id == 11) || (Id == 16) || (Id == 21))
                iCol = 1;
            if ((Id == 2) || (Id == 7) || (Id == 12) || (Id == 17) || (Id == 22))
                iCol = 2;
            if ((Id == 3) || (Id == 8) || (Id == 13) || (Id == 18) || (Id == 23))
                iCol = 3;
            if ((Id == 4) || (Id == 9) || (Id == 14) || (Id == 19) || (Id == 24))
                iCol = 4;

            return iCol;
        }
        private int GetRow(int Id)
        {
            int iRow = 0;

            if ((Id >= 0) && (Id <= 4))
                iRow = 0;
            if ((Id >= 5) && (Id <= 9))
                iRow = 1;
            if ((Id >= 10) && (Id <= 14))
                iRow = 2;
            if ((Id >= 15) && (Id <= 19))
                iRow = 3;
            if ((Id >= 20) && (Id <= 24))
                iRow = 4;

            return iRow;

        }

        private void OverlayImageEvent(Image MyImageOverlay)
        {
            var tapGestureRecognizer = new TapGestureRecognizer();

            tapGestureRecognizer.Tapped += (sender, e) =>
            {
                // handle the tap
                MyImage_Clicked(sender, e);
            };


            MyImageOverlay.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private async void WontheGameAlert()
        {
            bool answer;

            if(bIsBlackout == true)
            { 
                answer = await DisplayAlert("BLACKOUT!", "New Game?", "Yes", "No");

                if (answer == true)
                    Resetboard();
            }
            else
            { 
                answer = await DisplayAlert("BINGO!", "", "Continue--Play Blackout", "New Game");

                if (answer == true)
                {
                    bPlayBlackout = true;
                }
                else
                {
                    Resetboard();
                    bPlayBlackout = false;
                }
            }
        }
        #endregion Functions

        #region Events
        private void MyImage_Clicked(object sender, EventArgs e)
        {
            int Id;

            Image img = (Image)sender;

            //aBingoBoard[Convert.ToInt32(img.ClassId.Substring(0, 1)), Convert.ToInt32(img.ClassId.Substring(1, 1))] = "Y";
            Id = BingoList[Convert.ToInt32(img.ClassId)].Id;

            BingoList[Id].BlackoutClickedOn = "Y";

            if (BingoList[Id].ClickedOn == "N")
            { 
                BingoList[Id].ClickedOn = "Y";

                //img.Source = BingoList[25].image.Source;   //XT image  

                // **** New stuff
                Image MyImageOverlay = new Image();
                MyImageOverlay.ClassId = Id.ToString();
                MyImageOverlay.Aspect = Aspect.AspectFit;

                OverlayImageEvent(MyImageOverlay);
                MyImageOverlay.Source = BingoList[25].image.Source;
                
                Grid.SetColumn(MyImageOverlay, GetColumn(Id));
                Grid.SetRow(MyImageOverlay, GetRow(Id));

                //******************
                /*
                MyImageOverlay.Scale = 0;
                double playMyImageOverlayWidth = MyImageOverlay.Width;
                double maxScale = 1 / playMyImageOverlayWidth;

                MyImageOverlay.ScaleTo(maxScale, 1000, Easing.SpringOut);
                */
                //******************

                grdBingo.Children.Add(MyImageOverlay);
            }
            else
            {
                BingoList[Id].ClickedOn = "N";
                /*
                img.Scale = 0;
                double playMyImageOverlayWidth = img.Width;
                double maxScale = 1 / playMyImageOverlayWidth;
                //img.ScaleTo(maxScale, 1000, Easing.SpringOut);
                */
                img.Source = BingoList[Id].image.Source;

            }

            //******************
            bIsBlackout = true;

            for (int i = 0; i < 25; i++)
            {
                if (BingoList[i].BlackoutClickedOn == "N")
                {
                    bIsBlackout = false;
                    break;
                }
            }

            if (bPlayBlackout == false)
                bIsBlackout = false;

            if (bIsBlackout == true)
            {
                WontheGameAlert();
                return;
            }

            //******************
            //Horizontal Win

            if ((BingoList[0].ClickedOn == "Y") && (BingoList[1].ClickedOn == "Y") && (BingoList[2].ClickedOn == "Y") && (BingoList[3].ClickedOn == "Y") && (BingoList[4].ClickedOn == "Y"))
            {
                for (int i = 0; i < 5; i++)
                {
                    BingoList[i].ClickedOn = "N";
                    //BingoList[i].BlackoutClickedOn = "Y";
                }

                if(bPlayBlackout == false)
                    WontheGameAlert();
                
                //DisplayAlert("Congratulations", "You won the game", "OK");
            }

            if ((BingoList[5].ClickedOn == "Y") && (BingoList[6].ClickedOn == "Y") && (BingoList[7].ClickedOn == "Y") && (BingoList[8].ClickedOn == "Y") && (BingoList[9].ClickedOn == "Y"))
            {
                for (int i = 5; i < 10; i++)
                {
                    BingoList[i].ClickedOn = "N";
                    //BingoList[i].BlackoutClickedOn = "Y";
                }

                if (bPlayBlackout == false)
                    WontheGameAlert();
            }

            if ((BingoList[10].ClickedOn == "Y") && (BingoList[11].ClickedOn == "Y") && (BingoList[12].ClickedOn == "Y") && (BingoList[13].ClickedOn == "Y") && (BingoList[14].ClickedOn == "Y"))
            {
                for (int i = 10; i < 15; i++)
                {
                    BingoList[i].ClickedOn = "N";
                    //BingoList[i].BlackoutClickedOn = "Y";
                }

                if (bPlayBlackout == false)
                    WontheGameAlert();
            }

            if ((BingoList[15].ClickedOn == "Y") && (BingoList[16].ClickedOn == "Y") && (BingoList[17].ClickedOn == "Y") && (BingoList[18].ClickedOn == "Y") && (BingoList[19].ClickedOn == "Y"))
            {
                for (int i = 15; i < 20; i++)
                {
                    BingoList[i].ClickedOn = "N";
                    //BingoList[i].BlackoutClickedOn = "Y";
                }

                if (bPlayBlackout == false)
                    WontheGameAlert();
            }

            if ((BingoList[20].ClickedOn == "Y") && (BingoList[21].ClickedOn == "Y") && (BingoList[22].ClickedOn == "Y") && (BingoList[23].ClickedOn == "Y") && (BingoList[24].ClickedOn == "Y"))
            {
                for (int i = 20; i < 25; i++)
                {
                    BingoList[i].ClickedOn = "N";
                    //BingoList[i].BlackoutClickedOn = "Y";
                }

                if (bPlayBlackout == false)
                    WontheGameAlert();
            }

            //Vertical Win
            if ((BingoList[0].ClickedOn == "Y") && (BingoList[5].ClickedOn == "Y") && (BingoList[10].ClickedOn == "Y") && (BingoList[15].ClickedOn == "Y") && (BingoList[20].ClickedOn == "Y"))
            {
                for (int i = 0; i < 21; i += 5)
                {
                    BingoList[i].ClickedOn = "N";
                    //BingoList[i].BlackoutClickedOn = "Y";
                }

                if (bPlayBlackout == false)
                    WontheGameAlert();
            }

            if ((BingoList[1].ClickedOn == "Y") && (BingoList[6].ClickedOn == "Y") && (BingoList[11].ClickedOn == "Y") && (BingoList[16].ClickedOn == "Y") && (BingoList[21].ClickedOn == "Y"))
            {
                for (int i = 1; i < 22; i += 5)
                {
                    BingoList[i].ClickedOn = "N";
                    //BingoList[i].BlackoutClickedOn = "Y";
                }

                if (bPlayBlackout == false)
                    WontheGameAlert();
            }

            if ((BingoList[2].ClickedOn == "Y") && (BingoList[7].ClickedOn == "Y") && (BingoList[12].ClickedOn == "Y") && (BingoList[17].ClickedOn == "Y") && (BingoList[22].ClickedOn == "Y"))
            {
                for (int i = 2; i < 23; i += 5)
                {
                    BingoList[i].ClickedOn = "N";
                    //BingoList[i].BlackoutClickedOn = "Y";
                }

                if (bPlayBlackout == false)
                    WontheGameAlert();
            }

            if ((BingoList[3].ClickedOn == "Y") && (BingoList[8].ClickedOn == "Y") && (BingoList[13].ClickedOn == "Y") && (BingoList[18].ClickedOn == "Y") && (BingoList[23].ClickedOn == "Y"))
            {
                for (int i = 3; i < 24; i += 5)
                {
                    BingoList[i].ClickedOn = "N";
                    //BingoList[i].BlackoutClickedOn = "Y";
                }

                if (bPlayBlackout == false)
                    WontheGameAlert();
            }

            if ((BingoList[4].ClickedOn == "Y") && (BingoList[9].ClickedOn == "Y") && (BingoList[14].ClickedOn == "Y") && (BingoList[19].ClickedOn == "Y") && (BingoList[24].ClickedOn == "Y"))
            {
                for (int i = 4; i < 25; i += 5)
                {
                    BingoList[i].ClickedOn = "N";
                    //BingoList[i].BlackoutClickedOn = "Y";
                }

                if (bPlayBlackout == false)
                    WontheGameAlert();
            }


            //Diagonal from top to right
            if ((BingoList[0].ClickedOn == "Y") && (BingoList[6].ClickedOn == "Y") && (BingoList[12].ClickedOn == "Y") && (BingoList[18].ClickedOn == "Y") && (BingoList[24].ClickedOn == "Y"))
            {
                for (int i = 0; i < 25; i += 6)
                {
                    BingoList[i].ClickedOn = "N";
                    //BingoList[i].BlackoutClickedOn = "Y";
                }

                if (bPlayBlackout == false)
                    WontheGameAlert();
            }

            //Diagonal from top to left
            if ((BingoList[4].ClickedOn == "Y") && (BingoList[8].ClickedOn == "Y") && (BingoList[12].ClickedOn == "Y") && (BingoList[16].ClickedOn == "Y") && (BingoList[20].ClickedOn == "Y"))
            {
                for (int i = 4; i < 21; i += 4)
                {
                    BingoList[i].ClickedOn = "N";
                    //BingoList[i].BlackoutClickedOn = "Y";
                }

                if (bPlayBlackout == false)
                    WontheGameAlert();
            }
        }

        private void btnNewGame_Clicked(object sender, EventArgs e)
        {
            bPlayBlackout = false;
            Resetboard();
        }
        #endregion Events
    }
}

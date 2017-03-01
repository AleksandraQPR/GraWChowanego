using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zabawa_w_chowanego
{
    public partial class Form1 : Form
    {
        // Utworzenie referencji do pomieszczeń i lokalizacji
        Location currentLocation;

        RoomWithDoor livingRoom;
        Room diningRoom;
        RoomWithDoor kitchen;
        Room stairs;

        OutsideWithDoor frontYard;
        OutsideWithDoor backYard;
        OutsideWithHidingPlace garden;
        OutsideWithHidingPlace route;

        RoomWithHidingPlace bigBedroom;
        RoomWithHidingPlace smallBedroom;
        RoomWithHidingPlace bathroom;
        RoomWithHidingPlace corridor;

        // Utworzenie referencji przeciwnika
        Opponent opponent;
        Random random;

        // Ilość kroków, w czasie którch znaleiono przeciwnika
        int stepNumber = 1;

        public Form1()
        {
            InitializeComponent();
            CreateObjects();
            ResteGame();
        }

        private void CreateObjects()
        {
            // Parter
            livingRoom = new RoomWithDoor("salon", "antyczny dywan", "w szafie ściennej", "dębowe drzwi z mosiężną klamką");
            diningRoom = new Room("jadalnia", "kryształowy żyrandol");
            kitchen = new RoomWithDoor("kuchnia", "nierdzewne stalowe sztućce", "w szafce", "drzwi zasuwane");
            stairs = new Room("schody", "drewniana poręcz");

            // Zewnątrz
            frontYard = new OutsideWithDoor("podwórko przed domem", false, "dębowe drzwi z mosiężną klamką");
            backYard = new OutsideWithDoor("podwórko za domem", true, "drzwi zasuwane");
            garden = new OutsideWithHidingPlace("ogród", false, "w szopie");
            route = new OutsideWithHidingPlace("droga dojazdowa", false, "w garażu");

            // Piętro
            bigBedroom = new RoomWithHidingPlace("duża sypialnia", "duże łóżko", "pod dużym łóżkiem");
            smallBedroom = new RoomWithHidingPlace("mała sypialnia", "małe łóżko", "pod małym łóżkiem");
            bathroom = new RoomWithHidingPlace("łazienka", "umywalka i toaleta", "pod prysznicem");
            corridor = new RoomWithHidingPlace("korytarz", "obrazek z psem", "w szafie ściennej");

            // Ustawienie sąsiednich pomieszczeń poszczególnych lokalizacji
            livingRoom.Exits = new Location[] { diningRoom, stairs };
            diningRoom.Exits = new Location[] { livingRoom, kitchen };
            kitchen.Exits = new Location[] { diningRoom };
            stairs.Exits = new Location[] { livingRoom, corridor };
            corridor.Exits = new Location[] { stairs, bigBedroom, bathroom, smallBedroom };

            bigBedroom.Exits = new Location[] { corridor };
            bathroom.Exits = new Location[] { corridor };
            smallBedroom.Exits = new Location[] { corridor };

            frontYard.Exits = new Location[] { backYard, garden, route };
            backYard.Exits = new Location[] { garden, frontYard, route };
            garden.Exits = new Location[] { frontYard, backYard };
            route.Exits = new Location[] { frontYard, backYard };

            // Ustawienie przejść przez drzwi
            livingRoom.DoorLocation = frontYard;
            frontYard.DoorLocation = livingRoom;
            kitchen.DoorLocation = backYard;
            backYard.DoorLocation = kitchen;

        }

        private void MoveToANewLocation(Location newLocation)
        {
            this.currentLocation = newLocation;
            exits.Items.Clear();
            for(int i=0; i<this.currentLocation.Exits.Length; i++)
            {
                exits.Items.Add(this.currentLocation.Exits[i].Name);
            }
            exits.SelectedIndex = 0;
            description.Text = this.currentLocation.Description;
            RedrawForm();
        }

        private void goHere_Click(object sender, EventArgs e)
        {
            int itemIndex = exits.SelectedIndex;
            MoveToANewLocation(this.currentLocation.Exits[itemIndex]);
        }

        private void goThroughTheDoor_Click(object sender, EventArgs e)
        {
            IHasExteriorDoor newLocation;
            newLocation = this.currentLocation as IHasExteriorDoor;
            MoveToANewLocation(newLocation.DoorLocation);
        }

        private void check_Click(object sender, EventArgs e)
        {
            if (opponent.Check(this.currentLocation))
            {
                MessageBox.Show("Odnalazłeś mnie w " + stepNumber + " ruchach");
                ResteGame();
            }
            else
            {
                MessageBox.Show("Szukaj dalej");
                stepNumber++;
            }
        }

        private void hiding_Click(object sender, EventArgs e)
        {
            for(int i=1; i<11; i++)
            {
                description.Text = i + "\n";
                Application.DoEvents();
                System.Threading.Thread.Sleep(200);
                opponent.Move();
            }
            description.Text = "Gotowy czy nie - nadchodzę!";
            Application.DoEvents();
            System.Threading.Thread.Sleep(500);
            description.Text = "";

            MoveToANewLocation(livingRoom);     // Rozpoczynam grę stojąc w jadalni
        }

        private void RedrawForm()
        {
            // Ukrycie przycisku "Kryj się!"
            hiding.Visible = false;
            goHere.Visible = true;
            exits.Visible = true;

            // Bieżąca lokalizacja posiada dwrzwi zewnętrzne
            if (this.currentLocation is IHasExteriorDoor)
                goThroughTheDoor.Visible = true;
            else
                goThroughTheDoor.Visible = false;

            // Bieżąca lokalizacja posiada kryjówkę
            if (this.currentLocation is IHidingPlace)
            {
                IHidingPlace hidingPlace;
                hidingPlace = currentLocation as IHidingPlace;
                check.Text = "Sprawdź " + hidingPlace.PlaceToHide;
                check.Visible = true;
            }
            else
            {
                check.Visible = false;
            }
        }

        private void ResteGame()
        {
            goHere.Visible = false;
            exits.Visible = false;
            goThroughTheDoor.Visible = false;
            check.Visible = false;
            hiding.Visible = true;
            random = new Random();
            opponent = new Opponent(livingRoom, random); // Przeciwnik zaczyna grę stojąc na przednim podwórku
        }
    }
}

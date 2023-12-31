﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Pokemon_Guesser
{
    public partial class Form1 : Form
    {
        List<String> Pokemon = new List<String>();
        List<String> PokeDex = new List<String>();

        Random rnd = new Random();
        string DisplayPokemon, PokeName, number = string.Empty;
        StringBuilder Hint = new StringBuilder();
        int score;

        public Form1()
        {
            InitializeComponent();
        }

        public void MakePokemon()
        {
            int RNG = rnd.Next(Pokemon.Count);
            DisplayPokemon = Pokemon[RNG];
            PokeDisplay.Image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "/Data/Photos/" + PokeDex[RNG] + ".png");
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (DisplayPokemon == null || InputBox.Text == "") return;

            if (e.KeyCode == Keys.Return)
            {
                string Guess = InputBox.Text.ToString();
                InputBox.Clear();

                if (Guess == DisplayPokemon)
                {
                    score += 100;
                    OutputBox.Items.Insert(0, "The Pokémon was indeed " + Guess + "!");
                    MakePokemon();
                }
                else
                {
                    Hint.Clear();
                    for (int i = 0; i < DisplayPokemon.Length; i++) Hint.Append('-');

                    int numChar = 0;
                    int numhints = 0;
                    while (numhints < rnd.Next(3) + 1)
                    {
                        int RNG = (rnd.Next(4));
                        if (RNG == 0) 
                        {
                            Hint[numChar] = DisplayPokemon[numChar];
                            numhints++;
                        }

                        if (numChar == DisplayPokemon.Length - 1) numChar = 0;
                        else numChar++;
                    } 
                    OutputBox.Items.Insert(0, Hint);
                    score -= Int32.Parse(PunishAmount.Text);
                }
                ScoreBox.Text = score.ToString();
            }
        }

        private void SettingsSave_Click(object sender, EventArgs e)
        {
            StreamReader TxTFile = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/Data/Pokemon.txt");
            string FileLine = TxTFile.ReadLine();

            Pokemon.Clear();
            PokeDex.Clear();
            int PokeNum = 0;

            while (FileLine != null)
            {
                bool Vaild = true;

                if (!GenAll.Checked)
                {
                    if (!Gen1.Checked && ((PokeNum + 1) >= 1 && (PokeNum + 1) <= 151)) Vaild = false;
                    if (!Gen2.Checked && ((PokeNum + 1) >= 152 && (PokeNum + 1) <= 251)) Vaild = false;
                    if (!Gen3.Checked && ((PokeNum + 1) >= 252 && (PokeNum + 1) <= 386)) Vaild = false;
                    if (!Gen4.Checked && ((PokeNum + 1) >= 387 && (PokeNum + 1) <= 493)) Vaild = false;
                    if (!Gen5.Checked && ((PokeNum + 1) >= 494 && (PokeNum + 1) <= 649)) Vaild = false;
                    if (!Gen6.Checked && ((PokeNum + 1) >= 650 && (PokeNum + 1) <= 721)) Vaild = false;
                    if (!Gen7.Checked && ((PokeNum + 1) >= 722 && (PokeNum + 1) <= 809)) Vaild = false;
                    if (!Gen8.Checked && ((PokeNum + 1) >= 810 && (PokeNum + 1) <= 905)) Vaild = false;
                    if (!Gen9.Checked && ((PokeNum + 1) >= 906 && (PokeNum + 1) <= 1025)) Vaild = false;
                }

                if (Vaild)
                {
                    int i = 0;
                    PokeName = string.Empty;
                    number = string.Empty;

                    while (FileLine[i] != ',')
                    {
                        PokeName += FileLine[i];
                        i++;
                    }
                    Pokemon.Add(PokeName);
                    i++;
                    while (i < FileLine.Length)
                    {
                        number += FileLine[i];
                        i++;
                    }
                    PokeDex.Add(number);
                }

                FileLine = TxTFile.ReadLine();
                PokeNum++;
            }
            if (Pokemon.Count > 0) MakePokemon();
        }
    }
}

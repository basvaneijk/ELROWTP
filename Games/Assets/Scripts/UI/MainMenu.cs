using System;
using System.Collections.Generic;
using System.Globalization;
using Assets.Scripts.Helpers;
using Assets.Scripts.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    /// <summary>
    ///     Contains all the Main Menu (UI) logic.
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        /// <summary>
        ///     These fields contain the dimensions of the area which the player will be playing on. Used for level generation.
        /// </summary>
        private InputField inputLength, inputWidth;

        /// <summary>
        ///     The overlay panels for help and settings.
        /// </summary>
        private GameObject helpPanel, settingsPanel;

        /// <summary>
        ///     The action buttons for the overlay panels.
        /// </summary>
        private Transform helpPanelButtonClose, settingsPanelButtonClose, settingsPanelButtonSave;

        /// <summary>
        ///     The buttons used to select a player color.
        /// </summary>
        private Transform buttonBlue, buttonRed, buttonGreen;

        /// <summary>
        ///     The main menu buttons.
        /// </summary>
        private Transform buttonSettings, buttonHelp, buttonStart, buttonEnd;

        /// <summary>
        ///     The container for minigame tiles.
        /// </summary>
        private Transform minigameTiles;

        /// <summary>
        ///     Check the "PlayerPrefs" and set the corresponding sprites.
        /// </summary>
        private void Start()
        {
            RegisterComponents();

            //Disable overlay panels
            helpPanel.SetActive(false);
            settingsPanel.SetActive(false);

            RegisterListeners();

            //Retrieve the selected color from playerprefs
            string color = PlayerPrefs.GetString("color");
            if (String.Compare(color, "blue", StringComparison.OrdinalIgnoreCase) == 0)
            {
                buttonBlue.Find("Selected").gameObject.SetActive(true);
            }
            else if (String.Compare(color, "red", StringComparison.OrdinalIgnoreCase) == 0)
            {
                buttonRed.Find("Selected").gameObject.SetActive(true);
            }
            else if (String.Compare(color, "green", StringComparison.OrdinalIgnoreCase) == 0)
            {
                buttonGreen.Find("Selected").gameObject.SetActive(true);
            }

            LoadMinigames();
        }

        /// <summary>
        ///     Used to assign all global variables their values for easy references.
        /// </summary>
        private void RegisterComponents()
        {
            //Color buttons
            Transform buttonPanel = GameObject.FindGameObjectWithTag("MainMenu_UI_ColorButtons").transform;
            buttonBlue = buttonPanel.Find("Blue");
            buttonRed = buttonPanel.Find("Red");
            buttonGreen = buttonPanel.Find("Green");

            //Minigame tile wrapper
            minigameTiles = GameObject.FindGameObjectWithTag("MainMenu_UI_MinigameTiles").transform;

            //Menu buttons
            Transform controlButtons = GameObject.FindGameObjectWithTag("MainMenu_UI_ControlButtons").transform;
            buttonSettings = controlButtons.Find("ButtonSettings");
            buttonHelp = controlButtons.Find("ButtonHelp");
            buttonStart = controlButtons.Find("ButtonStart");
            buttonEnd = controlButtons.Find("ButtonEnd");

            //Panels
            helpPanel = GameObject.FindGameObjectWithTag("MainMenu_UI_HelpPanel");
            helpPanelButtonClose = helpPanel.transform.Find("ButtonClose");
            settingsPanel = GameObject.FindGameObjectWithTag("MainMenu_UI_SettingsPanel");
            settingsPanelButtonClose = settingsPanel.transform.Find("ButtonClose");
            settingsPanelButtonSave = settingsPanel.transform.Find("ButtonSave");

            inputLength = settingsPanel.transform.Find("InputLength").GetComponent<InputField>();
            inputWidth = settingsPanel.transform.Find("InputWidth").GetComponent<InputField>();
        }

        /// <summary>
        ///     Used to register listeners on buttons etc.
        /// </summary>
        private void RegisterListeners()
        {
            //Color buttons
            buttonBlue.GetComponent<Button>().onClick.AddListener(() => SetColor(Color.Blue));
            buttonRed.GetComponent<Button>().onClick.AddListener(() => SetColor(Color.Red));
            buttonGreen.GetComponent<Button>().onClick.AddListener(() => SetColor(Color.Green));

            //Panel action buttons
            helpPanelButtonClose.GetComponent<Button>().onClick.AddListener(ToggleHelp);
            settingsPanelButtonClose.GetComponent<Button>().onClick.AddListener(ToggleSettings);
            settingsPanelButtonSave.GetComponent<Button>().onClick.AddListener(SaveSettings);

            //Menu buttons
            buttonHelp.GetComponent<Button>().onClick.AddListener(ToggleHelp);
            buttonSettings.GetComponent<Button>().onClick.AddListener(ToggleSettings);
            buttonStart.GetComponent<Button>().onClick.AddListener(StartGame);
            buttonEnd.GetComponent<Button>().onClick.AddListener(EndGame);
        }

        /// <summary>
        ///     Loads the minigame information and adds loaded minigames to the UI.
        /// </summary>
        private void LoadMinigames()
        {
            List<MinigameInfo> minigames = Game.GetActiveMinigames();

            foreach (MinigameInfo minigame in minigames)
            {
                GameObject g = Instantiate(ResourceHelper.MinigameUiPanel);
                Minigame m = g.GetComponent<Minigame>();
                m.MinigameInfo = minigame;
                g.transform.SetParent(minigameTiles);
            }
        }

        //Used to start the selected minigame
        private void StartGame()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Used to launch a specific scene by id.
        /// </summary>
        /// <param name="id">The id of the scene to launch.</param>
        private void LaunchMinigame(int id)
        {
            Application.LoadLevel(id);
        }

        /// <summary>
        ///     Toggle the help screen. The help screen is used to inform the player of the possibilities.
        /// </summary>
        private void ToggleHelp()
        {
            if (helpPanel.activeSelf)
            {
                helpPanel.SetActive(false);
            }
            else
            {
                settingsPanel.SetActive(false);
                helpPanel.SetActive(true);
            }
        }

        /// <summary>
        ///     Toggle the settings screen. The settings screen is used to input the dimensions of the room.
        /// </summary>
        private void ToggleSettings()
        {
            if (settingsPanel.activeSelf)
            {
                inputLength.text = PlayerPrefs.GetFloat("length").ToString(CultureInfo.InvariantCulture);
                inputWidth.text = PlayerPrefs.GetFloat("width").ToString(CultureInfo.InvariantCulture);
                settingsPanel.SetActive(false);
            }
            else
            {
                helpPanel.SetActive(false);
                settingsPanel.SetActive(true);
            }
        }

        /// <summary>
        ///     Save the input from the settings screen. Saves only on submit.
        /// </summary>
        private void SaveSettings()
        {
            PlayerPrefs.SetFloat("width", float.Parse(inputWidth.text));
            PlayerPrefs.SetFloat("length", float.Parse(inputLength.text));
            ToggleSettings();
        }

        /// <summary>
        ///     End the game. Only works when build.
        /// </summary>
        private void EndGame()
        {
            Application.Quit();
        }

        /// <summary>
        ///     Sets the active color selection
        /// </summary>
        /// <param name="color">The color to select</param>
        private void SetColor(Color color)
        {
            //Reset color choice
            buttonBlue.Find("Selected").gameObject.SetActive(false);
            buttonRed.Find("Selected").gameObject.SetActive(false);
            buttonGreen.Find("Selected").gameObject.SetActive(false);

            //Write choice into player prefs
            PlayerPrefs.SetString("color", color.ToString());

            //Select choice
            switch (color)
            {
                case Color.Blue:
                    buttonBlue.Find("Selected").gameObject.SetActive(true);
                    break;
                case Color.Red:
                    buttonRed.Find("Selected").gameObject.SetActive(true);
                    break;
                case Color.Green:
                    buttonGreen.Find("Selected").gameObject.SetActive(true);
                    break;
            }
        }

        /// <summary>
        ///     The collection of possible collors to select
        /// </summary>
        private enum Color
        {
            Blue,
            Red,
            Green
        }
    }
}
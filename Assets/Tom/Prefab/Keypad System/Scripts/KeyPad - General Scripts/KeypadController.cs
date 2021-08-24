using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace keypadSystem
{
    public class KeypadController : MonoBehaviour
    {
        [Header("Keypad Parameters")]
        [SerializeField] private string validCode;
        public int characterLim;
        [HideInInspector] public bool firstClick;

        [Header("UI Elements")]
        public InputField codeText;
        // [SerializeField] private GameObject keyPadCanvas;
        [SerializeField] private UIController UIController;

        [Header("GameObjects")]
        [SerializeField] private GameObject keypadModel;

        [Header("Unlock Event")]
        [SerializeField] private UnityEvent unlock;

        private string[] AllValideCode = { "GAME", "HAND", "MOVE", "OPEN", "STAR" };

        [SerializeField]
        public GameObject gameObjectGenerateRandomNumber;

        private GenerateRandomNumber generateRandomNumber;
        public int theChoosenNum;

        public bool onKeyboard;

        public Camera CamToLoc;

        public QuestGiver QG;

        void OnTriggerEnter(Collider other)
        {
            onKeyboard = true;
        }

        void OnTriggerExit(Collider other)
        {
            onKeyboard = false;
        }

        // Start is called before the first frame update
        void Start()
        {
            generateRandomNumber = gameObjectGenerateRandomNumber.GetComponent<GenerateRandomNumber>();
            theChoosenNum = generateRandomNumber.theChoosenNum;
            validCode = AllValideCode[theChoosenNum];
        }

        void Update()
        {
            if (onKeyboard && Input.GetKeyDown(KeyCode.K))
            {
                if (UIController.keypadIsOpen) CloseKeypad();
                else ShowKeypad();
            }
        }

        public void CheckCode()
        {
            if (codeText.text == validCode)
            {
                keypadModel.tag = "Untagged";
                ValidCode();
                CloseKeypad();
            }

            else
            {
                KPAudioManager.instance.Play("KeypadDenied"); //0.2f
            }
        }

        void ValidCode()
        {
            //IF YOUR CODE IS CORRECT!
            unlock.Invoke();
            QG.openExplain();
        }

        public void ShowKeypad()
        {
            UIController.openKeypad();
            // keyPadCanvas.SetActive(true);
            // Cursor.visible = true;
            // Cursor.lockState = CursorLockMode.None;
            // CamToLoc.GetComponent<vThirdPersonCamera>().enabled = false;
            // lookx.enabled = false;
            // looky.enabled = false;
        }

        public void CloseKeypad()
        {
            UIController.closeKeypad();
            // keyPadCanvas.SetActive(false);
            // Cursor.visible = false;
            // Cursor.lockState = CursorLockMode.Locked;
            // CamToLoc.GetComponent<vThirdPersonCamera>().enabled = true;
            // lookx.enabled = true;
            // looky.enabled = true;
        }

        public void SingleBeep()
        {
            KPAudioManager.instance.Play("KeypadBeep");
        }

        void OnGUI()
        {
            GUIStyle gustyle = new GUIStyle(GUI.skin.box);
            gustyle.fontSize = 40;
            if (onKeyboard)
            {
                if (!UIController.keypadIsOpen)
                {
                    GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 60, 600, 50), "Press K to Open the Keyboard", gustyle);
                }
                else
                {
                    GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 60, 600, 50), "Press K to Close the Keyboard", gustyle);
                }

            }
        }
    }
}

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
        [SerializeField] private GameObject keyPadCanvas;

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
            if (onKeyboard && Input.GetKeyDown(KeyCode.E))
            {
                ShowKeypad();
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
        }

        public void ShowKeypad()
        {
            keyPadCanvas.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            CamToLoc.GetComponent<vThirdPersonCamera>().enabled = false;
            // lookx.enabled = false;
            // looky.enabled = false;
        }

        public void CloseKeypad()
        {
            keyPadCanvas.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            CamToLoc.GetComponent<vThirdPersonCamera>().enabled = true;
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
                GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 60, 600, 50), "Press E to Open the Keyboard", gustyle);
                
            }
        }
    }
}

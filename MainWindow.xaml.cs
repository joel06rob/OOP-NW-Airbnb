using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OOP_NW_Airbnb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TestChatbot();
            
        }

        //Testing 11/03/26 SearchQuestions - Testing 16/03/26 GetQuestionByID
        public void TestChatbot()
        {
            string userInput = "How do i turn on the heating";
            List<Question> testresult;

            ChatBot cb = new ChatBot();
            testresult = cb.ProcessInput(userInput);
            
            Debug.WriteLine("TestChatbot: " + testresult);
        }

        private void btnContact_Click(object sender, RoutedEventArgs e)
        {
            ContactChat contactChat = new ContactChat();
            contactChat.Show();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}
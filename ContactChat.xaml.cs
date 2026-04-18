using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OOP_NW_Airbnb
{
    /// <summary>
    /// Interaction logic for ContactChat.xaml
    /// </summary>
    public partial class ContactChat : Window
    {
        private ChatBot bot = new ChatBot();
        private List<ChatMessage> messages = new List<ChatMessage>();
        private List<Question> lastResults = new List<Question>();

        //Handling other option
        private bool isInOtherTriage = false;
        private int otherTriageStep = 0;


        public ContactChat()
        {
            InitializeComponent();

            //Welcome Message
            BotMessage("Hello! Ask me anything about the property.");

            RefreshChat();
        }

        private void btnChatInput_Click(object sender, RoutedEventArgs e)
        {
            string input = txtChatInput.Text;

            //If message is blank, do nothing.
            if (string.IsNullOrEmpty(input)) return;



            //User - Add new message and process users input if input isn't blank
            messages.Add(new ChatMessage { Message = input, IsUser = true });

            if (isInOtherTriage)
            {
                OtherTriageQuestions();
                RefreshChat();
                txtChatInput.Clear();
                return;
            }

            //If user types Other, start triage questions to direct them to the correct contact method
            if (input.ToLower() == "other")
            {
                isInOtherTriage = true;
                otherTriageStep = 1;
                OtherTriageQuestions();
                RefreshChat();
                txtChatInput.Clear();
                return;
            }

            //User - Check if user typed a number to select a question
            if (int.TryParse(input, out int choice) && lastResults.Count > 0)
            {
                //Check if the choice is valid, if so create a new message with the answer
                if (choice > 0 && choice <= lastResults.Count)
                {
                    var selectedQuestion = lastResults[choice - 1];

                    BotMessage(selectedQuestion.Answer);
                }
                else
                {
                    BotMessage("Invalid Selection - Please type the ID of the question!");
                }
            }
            else
            {

                //Bot - Method to send a thankyou message on exit / thankyou text
                bool exitTriggered = false;
                string[] chatExitKWs = { "thanks", "thankyou", "bye", "exit" };
                for (int i = 0; i < chatExitKWs.Length; i++)
                {
                    if (input.ToLower().Contains(chatExitKWs[i]))
                    {
                        BotMessage("Thank you for chatting! If you have any more questions, feel free to contact the owner directly.");
                        break;
                    }
                }

                lastResults = bot.ProcessInput(input);
               

                //Bot - Send response message
                if (lastResults.Count > 0)
                {
                    BotMessage("Here are some related questions");

                    //Show numbered options of questions
                    for (int i = 0; i < lastResults.Count; i++)
                    {
                        messages.Add(new ChatMessage
                        {
                            Message = $"{i + 1}. {lastResults[i].QuestionText}",
                            IsUser = false
                        });
                    }
                    BotMessage("Type the number ID for the question to see the answer.");
                }
                else
                {
                    BotMessage("Sorry, I couldn't find anything about that. Please contact the owner directly.");
                }
            }

            RefreshChat();

            txtChatInput.Clear();


        }

        private void BotMessage(string message_text)
        {
            messages.Add(new ChatMessage
            {
                Message = message_text,
                IsUser = false
            });
        }

        private void OtherTriageQuestions()
        {

            switch (otherTriageStep)
            {
                case 1:
                    //Question 1
                    BotMessage("Is this a serious emergency?");
                    otherTriageStep = 2;
                    break;
                case 2:
                    //Question 2
                    BotMessage("Have you tried asking the chatbot your question? (yes/no)");
                    otherTriageStep = 3;
                    break;
                case 3:
                    BotMessage("Please contact the owner directly at: owner@nwairbnb.co.uk or 07799 654321");
                    
                    isInOtherTriage = false;
                    otherTriageStep = 0;
                    break;
            }

        }

        private void RefreshChat()
        {
            lstChat.ItemsSource = null;
            lstChat.ItemsSource = messages;

            //Add an auto scroll to the bottom of the chat
        }

        private void QuickQuestion_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            txtChatInput.Text = btn.Content.ToString();
        }
    }
}

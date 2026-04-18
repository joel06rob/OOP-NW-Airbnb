using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace OOP_NW_Airbnb
{

    public class ChatBot
    {
        private QuestionRepository repository = new QuestionRepository();

        //METHOD: Process User Input
        public List<Question> ProcessInput(string userInput)
        {
            userInput = userInput.Replace("?", string.Empty);
            List<string> keywords = ExtractKeywords(userInput);
            Debug.WriteLine("Keywords:");
            Debug.WriteLine(string.Join(",", keywords));

            return repository.SearchQuestions(keywords);
        }

        //METHOD: Extract individual keywords from user input
        private List<string> ExtractKeywords(string input)
        {
            return input.ToLower().Split(' ').ToList();
        }

    }
}

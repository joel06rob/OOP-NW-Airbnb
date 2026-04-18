using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace OOP_NW_Airbnb
{
    public class QuestionRepository
    {
        private Database db = new Database();
        

        //METHOD: Search Question
        //Search for question in database
        //Return list of questions that match search criteria
        //TODO: Can this method return a list box / selection the user can click on to return a question
        public List<Question> SearchQuestions(List<String> keywords) 
        {

            List<Question> questions = new List<Question>();

            using(SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                string keywordParams = string.Join(",", keywords.Select((k, i) => "@kw" + i));
                cmd.CommandText = $@"SELECT DISTINCT q.QuestionId, q.QuestionText, q.Answer FROM Questions q JOIN Keywords k ON q.QuestionId = k.QuestionId WHERE k.Word IN ({keywordParams})";

                for(int i = 0; i < keywords.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@kw" + i, keywords[i]);
                }

                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    //Append results to list of questions
                    questions.Add(new Question
                    {
                        QuestionId = (int)reader["QuestionId"],
                        QuestionText = reader["QuestionText"].ToString(),
                        Answer = reader["Answer"].ToString()
                    });

                    //TEST- Print out values
                    Debug.WriteLine(reader["QuestionID"].ToString());
                    Debug.WriteLine(reader["QuestionText"]);
                    Debug.WriteLine(reader["Answer"]);
                }

            }

            
            return questions;
        }




        //METHOD: Get Question / Answer by ID when user selects a question from the search results.
        public Question GetQuestionById(int id)
        {

            //Test 16/03/26 - Print out answer values from selected question ID

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;                                     //16/03/26 Test - Change to inputted or selected Question ID
                cmd.CommandText = "SELECT DISTINCT Answer FROM Questions WHERE QuestionId = @id";
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    //Append question
                    return new Question
                    {
                        QuestionId = (int)reader["QuestionId"],
                        QuestionText = reader["QuestionText"].ToString(),
                        Answer = reader["Answer"].ToString()
                    };


                    //TEST- Print out values
                    Debug.WriteLine("The answer is" + reader["Answer"]);
                }
            }



                return null;
        }




        //Below, Here.
        //METHODS: ADMIN CRUD FUNCTIONS BELOW

        //Create
        
    }
}

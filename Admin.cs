using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_NW_Airbnb
{
    public class Admin
    {
        private Database db = new Database();

        //ADMIN CRUD METHODS
        //
        //TODO: Add keywords to each function
        //

        public void AddQuestion(Question question, List<Keyword> keywords)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = "INSERT INTO Questions (QuestionText, Answer) VALUES (@q, @a)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@q", question.QuestionText);
                cmd.Parameters.AddWithValue("@a", question.Answer);

                int questionId = (int)cmd.ExecuteScalar();

                //Insert keywords
                if (keywords != null && keywords.Count > 0)
                {
                    foreach (Keyword keyword in keywords)
                    {
                        string keywordQuery = "INSERT INTO Keywords (KeywordId, Word, QuestionId) VALUES (@word, @qId)";
                        SqlCommand kCmd = new SqlCommand(keywordQuery, conn);
                        kCmd.Parameters.AddWithValue("@word", keyword.Word);
                        kCmd.Parameters.AddWithValue("@qId", questionId);

                        kCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public List<Question> GetQuestions()
        {
            List<Question> questions = new List<Question>();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Questions";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    questions.Add(new Question
                    {
                        QuestionId = (int)reader["Id"],
                        QuestionText = reader["QuestionText"].ToString(),
                        Answer = reader["Answer"].ToString()
                    });
                }
            }

            return questions;
        }

        public void UpdateQuestion(Question question)
        {
            
        }

        public void DeleteQuestion(int questionId)
        {
            using (SqlConnection conn = db.GetConnection()) {

                //1. Delete keywords associated with the question
                string deleteKeywords = "DELETE FROM Keywords WHERE QuestionId = @Id";
                SqlCommand kCmd = new SqlCommand(deleteKeywords, conn);
                kCmd.Parameters.AddWithValue("@id", questionId);
                kCmd.ExecuteNonQuery();

                //2. Delete the question itself
                string deleteQuestion = "DELETE FROM Questions WHERE QuestionId = @Id";
                SqlCommand qCmd = new SqlCommand(deleteQuestion, conn);
                qCmd.Parameters.AddWithValue("@id", questionId);
                qCmd.ExecuteNonQuery();
            }

        }
    }
}

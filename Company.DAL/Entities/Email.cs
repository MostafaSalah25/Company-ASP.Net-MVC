using System;


namespace Company.DAL.Entities
{
    public class Email
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
    }
}

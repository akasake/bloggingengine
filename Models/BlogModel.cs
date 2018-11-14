using System;
using System.Collections.Generic;

namespace BloggingEngine.Models
{
   public class BlogModel
    {
        public int Id { get; set; }
        public List<Post> Posts { get; set; }
        public int AuthorId { get; set; }
        public PersonModel Author { get; set; }
        public string Name { get; set; }  
        
    }

public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        internal DateTime Date { get; set; }
        public int BlogId { get; set; }
        public BlogModel Blog { get; set; }
        public List<Comment> Comments { get; set; }

    }

    public class PostWithComment{
        public Comment NewComment { get; set; }
        public Post Post { get; set; }
    }
    public class Comment 
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int AuthorId { get; set; }
        public PersonModel Author { get; set; }
        public String Content { get; set; }
    }

    public class PersonModel
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        
    }
    public class PeopleList{
        public List<PersonModel> Peoples { get; set; }
    }


    public class BlogPostList{
        public List<Post> Posts { get; set; }
        public string BlogTitle { get; set; }  
        public PersonModel Author { get; set; }
        
    }
    public class BlogList{
        public List<BlogModel> Blogs { get; set; }
    }

}
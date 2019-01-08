using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BloggingEngine.DataAccess;

namespace BloggingEngine.Models
{
   public class BlogModelModel
    {
        public int Id { get; set; }
        public List<PostModel> Posts { get; set; }
        public int AuthorId { get; set; }
        public PersonModel Author { get; set; }

        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }  
        
    }

public class PostModel
    {
        public int Id { get; set; }
        
        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [StringLength(1000, MinimumLength = 3)]
        [Required]
        public string Content { get; set; }
        public int BlogId { get; set; }
        public BlogModel Blog { get; set; }
        public List<Comment> Comments { get; set; }
        public String Date {get; set;}

    }

    public class PostWithCommentModel{
        public CommentModelModel NewComment { get; set; }
        public PostModel Post { get; set; }
    }
    public class CommentModelModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int AuthorId { get; set; }
        public PersonModelModel Author { get; set; }
        [StringLength(1000, MinimumLength = 3)]
        [Required]
        public String Content { get; set; }
        public String Date {get; set;}
    }

    public class PersonModelModel
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public String FirstName { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public String LastName { get; set; }
        
    }
    public class PeopleListModel{
        public List<PersonModel> Peoples { get; set; }
    }


    public class BlogPostListModel{
        public List<PostModel> Posts { get; set; }
        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string BlogTitle { get; set; }  
        public PersonModelModel Author { get; set; }
        
    }
    public class BlogListModel{
        public List<BlogModelModel> Blogs { get; set; }
    }

    public class BlogAndPeopleModel{
        public BlogModelModel Blog{ get; set; }
        public List<PersonModel> People { get; set; }
    }

    public class PostWithComment{
        public CommentModelModel NewComment { get; set; }
        public PostModel Post { get; set; }
        public List<PersonModel> People { get; set; }
    }

}
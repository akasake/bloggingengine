using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BloggingEngine.Models;
using BloggingEngine.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CSharp_Pline.Controllers;

namespace BloggingEngine.Controllers
{
    public class BloggingController : Controller
    {
        private BloggingContext _bloggingContext;
        private readonly ILogger<BloggingController> _logger;

        public BloggingController(BloggingContext bloggingContext, ILogger<BloggingController> logger)
        {
            _bloggingContext = bloggingContext;
            _logger = logger;
        }

        // see list of blogs
        [Route("blogging")]
        [PokeActionFilter]
         public IActionResult Index()
        {
            _logger.LogInformation("Index page says hello");
            var allBlogs = _bloggingContext.Blogs.Include(b => b.Author).ToList();
            var blogList = new BlogList(); 
            blogList.Blogs = allBlogs;
            return View(blogList);
        }

        //create new Author
        [PokeActionFilter]
        [Route("blogging/author/create")]
        [HttpGet()]
        public IActionResult CreateAuthor(){
            var emptyPerson = new PersonModelModel();
            return View(emptyPerson);
        }

        //save new Author
        [Route("blogging/author/create")]
        [PokeActionFilter]
        [HttpPost()]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAuthor(PersonModelModel person){
            var newPerson = new BloggingEngine.DataAccess.PersonModel(){
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                };
            if(ModelState.IsValid){
                
                _bloggingContext.People.Add(newPerson);
                _bloggingContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
            
        }

        // edit blog show
        [Route("blogging/edit/blog/{id}")]
        [PokeActionFilter]
        [HttpGet()]
        public IActionResult EditBlog([FromRoute] int Id)
        { 
            var blog = _bloggingContext.Blogs.Find(Id);
            var blogModel = new BlogModelModel(){
                Name = blog.Name,
            };
            if(blog == null){
                return RedirectToAction("Index");
            };
            return View(blogModel);
        }

        // saving edited blog
        [Route("blogging/edit/blog/{id}")]
        [HttpPost()]
        [PokeActionFilter]
        [ValidateAntiForgeryToken]
        public IActionResult EditBlog([FromRoute] int Id, BlogModelModel afterBlog)
        { 
            var toUpdateBlog = _bloggingContext.Blogs.Find(Id);
                toUpdateBlog.Name = afterBlog.Name;
            if(ModelState.IsValid){
                
                _bloggingContext.Blogs.Update(toUpdateBlog);
                _bloggingContext.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View(afterBlog);
        }


        //create new blog
        [Route("blogging/create")]
        [HttpGet()]
        [PokeActionFilter]
        public IActionResult CreateBlog(){
            var emptyBlog = new BlogModelModel();
            var people = _bloggingContext.People.ToList();
            var ep = new BlogAndPeopleModel(){
                Blog = emptyBlog,
                People = people,
            };
            return View(ep);
        }

        //save new blog
        [Route("blogging/create")]
        [HttpPost()]
        [PokeActionFilter]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBlog(BlogModelModel blog){
            var newBlog = new BloggingEngine.DataAccess.BlogModel(){
                    AuthorId = blog.AuthorId,
                    Name = blog.Name
                };
            if(ModelState.IsValid){
                
                _bloggingContext.Blogs.Add(newBlog);
                _bloggingContext.SaveChanges();
                return RedirectToAction("Index");
            }
            var people = _bloggingContext.People.ToList();
            var ep = new BlogAndPeopleModel(){
                Blog = blog,
                People = people,
            };
            return View(ep);
        }
        

        // see all post from one blog by id 
        [Route("blogging/blog/{id}")]
        [HttpGet]
        [PokeActionFilter]
        public IActionResult Posts([FromRoute]int Id)
        {
            //get posts of blog
            var postsList = _bloggingContext.Posts.Where(_bloggingContext => _bloggingContext.BlogId == Id).ToList();
            // get blog
             var blog = _bloggingContext.Blogs.Find(Id);
             //get author of blog
             if(blog == null){
                return RedirectToAction("Index");
            };
             var author = _bloggingContext.People.Find(blog.AuthorId);
            // build author model
            var authorModel = new PersonModel() {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            }; 
            //build blogpostListModel
            var posts = new BlogPostList(){
                Posts = postsList,
                BlogTitle = blog.Name,
                BlogId = blog.Id,
                Author = authorModel
            };

            return View(posts); 
        }
        //edit post
        [Route("blogging/edit/post/{id}")]
        [HttpGet]
        [PokeActionFilter]
        public IActionResult EditPost([FromRoute]int Id)
        {
            var post = _bloggingContext.Posts.Find(Id);
            var postModel = new PostModel {
                Id= post.Id,
                Title = post.Title,
                Content = post.Content,
                BlogId = post.BlogId,

            };
            if(post == null){
                return RedirectToAction("Index");
            };
            

            return View(postModel);
        }

        // save edit post
        [Route("blogging/edit/post/{id}")]
        [HttpPost]
        [PokeActionFilter]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost([FromRoute]int Id, PostModel post)
        {
            
            if(ModelState.IsValid){
                var toUpdatepost = _bloggingContext.Posts.Find(post.Id);
                toUpdatepost.Id = post.Id;
                toUpdatepost.Title = post.Title;
                toUpdatepost.Content = post.Content;
                toUpdatepost.BlogId = post.BlogId;
                //toUpdatepost.Date = DateTime.Now.ToString("dd/MM/yyyy");
                _bloggingContext.Posts.Update(toUpdatepost);
                _bloggingContext.SaveChanges();
                return RedirectToAction("posts");
            }
            return View(post);

        }


        //create new post
        [Route("blogging/create/blog/{id}/post")]
        [HttpGet()]
        [PokeActionFilter]
        public IActionResult CreatePost([FromRoute]int Id){
            var emptyPost = new PostModel();
            emptyPost.BlogId = Id;
            return View(emptyPost);
        }

        //save new post
        [Route("blogging/create/blog/{id}/post")]
        [HttpPost()]
        [PokeActionFilter]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(PostModel post){
            var newPost = new BloggingEngine.DataAccess.Post(){
                    Title = post.Title,
                    Content = post.Content,
                    BlogId = post.BlogId,
                    Date = System.DateTime.Now.ToString("dd MMMM yyy"),
                };
            if(ModelState.IsValid){     
                _bloggingContext.Posts.Add(newPost);
                _bloggingContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // show details of a Post
        [Route("blogging/post/{id}")]
        [HttpGet]
        [PokeActionFilter]
        public IActionResult PostDetail([FromRoute]int Id)
        {
            var post = _bloggingContext.Posts.Find(Id);
            if(post == null){
                return RedirectToAction("Index");
            };
            var comments = _bloggingContext.Comments.Where(_bloggingContext => _bloggingContext.PostId == Id).Include(c => c.Author).ToList();
            var postModel = new PostModel(){
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                BlogId = post.BlogId,
                Blog = post.Blog,
                Comments = comments,
                Date = post.Date
            };
            var people = _bloggingContext.People.ToList();
            var newcomment = new CommentModelModel();
            var pc = new PostWithComment(){
                NewComment = newcomment,
                Post = postModel,
                People = people
            };

            return View(pc);
        }

        // Add comment
        [Route("blogging/post/{id}")]
        [HttpPost]
        [PokeActionFilter]
        public IActionResult Comment([FromRoute]int Id, PostWithComment item)
        {
            if(ModelState.IsValid){
                var newcomment = new BloggingEngine.DataAccess.Comment() {
                    AuthorId = item.NewComment.AuthorId,
                    Content = item.NewComment.Content,
                    PostId = Id,
                    Date = System.DateTime.Now.ToString("hh:mm tt - dd MMMM yyy"),
                };
                _bloggingContext.Comments.Add(newcomment);
                _bloggingContext.SaveChanges();
                return RedirectToAction("postdetail");
            }
            return RedirectToAction("postdetail");
            
        }

        // delete post page
        [Route("blogging/post/{id}/delete")]
        [HttpGet]
        [PokeActionFilter]
        public IActionResult DeletePost([FromRoute]int Id)
        {
            var post = _bloggingContext.Posts.Find(Id);
            if(post == null){
                return RedirectToAction("Posts");
            };
            return View(post);
        }

        //actually deleting post
        [Route("blogging/post/{id}/delete")]
        [HttpPost]
        [PokeActionFilter]
        public IActionResult DeletePost([FromRoute]int Id, string confirmation)
        {
            var post = _bloggingContext.Posts.Find(Id);
            var comments = _bloggingContext.Comments.Where(c => c.PostId == Id).ToList();
            foreach(Comment comment in comments){
                _bloggingContext.Remove(comment);
            }
            _bloggingContext.Remove(post);
            _bloggingContext.SaveChanges();
            return RedirectToAction("Posts");
        }
    }
}
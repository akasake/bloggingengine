using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BloggingEngine.DataAccess;

namespace BloggingEngine.Controllers
{
    public class BloggingController : Controller
    {
        private BloggingContext _bloggingContext;

        public BloggingController(BloggingContext bloggingContext)
        {
            _bloggingContext = bloggingContext;
        }

        // see list of blogs
        [Route("blogging")]
         public IActionResult Index()
        { 
            var allBlogs = _bloggingContext.Blogs.ToList();
            foreach(BlogModel blog in allBlogs){
                var author = _bloggingContext.People.Find(blog.AuthorId);
                blog.Author= author;
            }
            var blogList = new BlogList(); 
            blogList.Blogs = allBlogs;
            return View(blogList);
        }

        // edit blog show
        [Route("blogging/edit/blog{id}")]
        [HttpGet()]
        public IActionResult EditBlog([FromRoute] int Id)
        { 
            var blog = _bloggingContext.Blogs.Find(Id);
            return View(blog);
        }

        // saving edited blog
        [Route("blogging/edit/blog{id}")]
        [HttpPost()]
        public IActionResult EditBlog([FromRoute] int Id, BlogModel afterBlog)
        { 
            var toUpdateBlog = _bloggingContext.Blogs.Find(Id);
            toUpdateBlog.Name = afterBlog.Name;
            _bloggingContext.Blogs.Update(toUpdateBlog);
            _bloggingContext.SaveChanges();
            return RedirectToAction("Index");
        }


        //create new blog
        [Route("blogging/create")]
        [HttpGet()]
        public IActionResult CreateBlog(){
            var emptyBlog = new BlogModel();
            var people = _bloggingContext.People.ToList();
            var ep = new BlogAndPeople(){
                Blog = emptyBlog,
                People = people
            };
            return View(ep);
        }

        //save new blog
        [Route("blogging/create")]
        [HttpPost()]
        public IActionResult CreateBlog(BlogModel blog){
            var newBlog = new BloggingEngine.DataAccess.BlogModel(){
                AuthorId = blog.AuthorId,
                Name = blog.Name
            };
            _bloggingContext.Blogs.Add(newBlog);
            _bloggingContext.SaveChanges();
            return RedirectToAction("Index");
        }
        

        // see all post from one blog by id 
        [Route("blogging/blog{id}")]
        [HttpGet]
        public IActionResult Posts([FromRoute]int Id)
        {
            //get posts of blog
            var postsList = _bloggingContext.Posts.Where(_bloggingContext => _bloggingContext.BlogId == Id).ToList();
            // get blog
             var blog = _bloggingContext.Blogs.Find(Id);
             //get author of blog
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
        [Route("blogging/edit/post{id}")]
        [HttpGet]
        public IActionResult EditPost([FromRoute]int Id)
        {
            var post = _bloggingContext.Posts.Find(Id);

            return View(post);
        }

        // save edit post
        [Route("blogging/edit/post{id}")]
        [HttpPost]
        public IActionResult EditPost([FromRoute]int Id, Post post)
        {
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


        //create new post
        [Route("blogging/create/blog{id}/post")]
        [HttpGet()]
        public IActionResult CreatePost([FromRoute]int Id){
            var emptyPost = new Post();
            emptyPost.BlogId = Id;
            return View(emptyPost);
        }

        //save new post
        [Route("blogging/create/blog{id}/post")]
        [HttpPost()]
        public IActionResult CreateBlog(Post post){
            var newPost = new BloggingEngine.DataAccess.Post(){
                Title = post.Title,
                Content = post.Content,
                BlogId = post.BlogId,
            };
            _bloggingContext.Posts.Add(newPost);
            _bloggingContext.SaveChanges();
            return RedirectToAction("Posts");
        }

        // show details of a Post
        [Route("blogging/post{id}")]
        [HttpGet]
        public IActionResult PostDetail([FromRoute]int Id)
        {
            var post = _bloggingContext.Posts.Find(Id);
            var comments = _bloggingContext.Comments.Where(_bloggingContext => _bloggingContext.PostId == Id).ToList();
            foreach(Comment comment in comments){
                var author = _bloggingContext.People.Find(comment.AuthorId);
                comment.Author= author;
            }
            post.Comments= comments;
            var people = _bloggingContext.People.ToList();
            var newcomment = new Comment();
            var pc = new PostWithComment(){
                NewComment = newcomment,
                Post = post,
                People = people
            };

            return View(pc);
        }

        // Add comment
        [Route("blogging/post{id}")]
        [HttpPost]
        public IActionResult Comment([FromRoute]int Id, PostWithComment item)
        {
            var newcomment = new BloggingEngine.DataAccess.Comment() {
                AuthorId = item.NewComment.AuthorId,
                Content = item.NewComment.Content,
                PostId = Id
            };
            _bloggingContext.Comments.Add(newcomment);
            _bloggingContext.SaveChanges();

            return RedirectToAction("postdetail");
        }

        // delete post page
        [Route("blogging/post{id}/delete")]
        [HttpGet]
        public IActionResult DeletePost([FromRoute]int Id)
        {
            var post = _bloggingContext.Posts.Find(Id);
            return View(post);
        }

        //actually deleting post
        [Route("blogging/post{id}/delete")]
        [HttpPost]
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
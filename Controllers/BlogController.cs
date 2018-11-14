using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BloggingEngine.Models;

/*namespace BloggingEngine.Controllers
{
    public class BlogController : Controller
    {
         public IActionResult Index()
        {
            PersonModel p1 = new PersonModel() { firstname = "Pham", lastname = "Van den Eynden"};
            PersonModel p2 = new PersonModel() { firstname = "Steven", lastname = "The pooper"};
            PersonModel p3 = new PersonModel() { firstname = "Lisa", lastname = "The Coole"};
            var listModel = new BlogPostList() {
                Posts = new System.Collections.Generic.List<BlogModel>()
            };
            listModel.Posts.Add(new BlogModel(){
                id = 1,
                Author  = p1, 
                title = "hello", 
                date = DateTime.Now, 
                content = "Beep beep I'm a sheep I said beep beep I'm a sheep Beep beep I'm a sheep I said beep beep I'm a sheep BEEP BEEP I'm a sheep I said beep beep I'm a sheep BEEP BEEP I'm a sheep I said beep beep I'm a sheep"
            });
            listModel.Posts.Add(new BlogModel(){
                id=2,
                Author  = p2, 
                title = "this", 
                date = DateTime.Now, 
                content = "Step one Throw your hands up And point them to the floor Step two Here's what to do Now get down on all fours Step three Just bounce around It's easy, follow me Step four Go crazy now! And beep beep like a sheep!"
            });
            listModel.Posts.Add(new BlogModel(){
                id=3,
                Author  = p3, 
                title = "is", 
                date = DateTime.Now, 
                content = "The challenge for ISFJs is ensuring that what they do is noticed. They have a tendency to underplay their accomplishments, and while their kindness is often respected, more cynical and selfish people are likely to take advantage of ISFJs’ dedication and humbleness by pushing work onto them and then taking the credit. ISFJs need to know when to say no and stand up for themselves if they are to maintain their confidence and enthusiasm. Naturally social, an odd quality for Introverts, ISFJs utilize excellent memories not to retain data and trivia, but to remember people, and details about their lives. When it comes to gift-giving, ISFJs have no equal, using their imagination and natural sensitivity to express their generosity in ways that touch the hearts of their recipients. While this is certainly true of their coworkers, whom people with the ISFJ personality type often consider their personal friends, it is in family that their expressions of affection fully bloom."
            });
            listModel.Posts.Add(new BlogModel(){
                id = 4,
                Author  = p1, 
                title = "BLEEEEEP", 
                date = DateTime.Now, 
                content = "BLA bla blablabalbalbala "
            });
            listModel.Posts.Add(new BlogModel(){
                id = 5,
                Author  = p2, 
                title = "Poo", 
                date = DateTime.Now, 
                content = "BLA bla blablabalbalbala "
            });
            return View(listModel);
        }
            //new BlogModel() { Author  = p1, title = "hello", date = DateTime.Now, content = " "}
        public IActionResult Detail([FromRoute] int blogId){
            PersonModel p1 = new PersonModel() { firstname = "Pham", lastname = "Van den Eynden"};
            var blogPost = new BlogModel(){
                id = 1,
                Author  = p1, 
                title = "Poo",
                date = DateTime.Now, 
                content = "BLA bla blablabalbalbala"
            }; 
            return View(blogPost);
        }
        public IActionResult Edit([FromRoute] int blogId){
            PersonModel p1 = new PersonModel() { firstname = "Pham", lastname = "Van den Eynden"};
            var blogPost = new BlogModel(){
                id = 1,
                Author  = p1, 
                title = "Poo",
                date = DateTime.Now, 
                content = "BLA bla blablabalbalbala"
            }; 
            return View(blogPost);
        }
        public IActionResult Delete([FromRoute] int blogId){
            PersonModel p1 = new PersonModel() { firstname = "Pham", lastname = "Van den Eynden"};
            var blogPost = new BlogModel(){
                id = 1,
                Author  = p1, 
                title = "Poo",
                date = DateTime.Now, 
                content = "BLA bla blablabalbalbala"
            }; 
            return View(blogPost);
        }


            
    }             
        // we moeten een model maken: list <T> type blogpost: titel, inhoud, auteur, datum
    
}*/
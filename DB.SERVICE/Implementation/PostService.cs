using BD.DATA.Entity;
using BD.REPO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB.SERVICE.Implementation
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> PostRepository;
        public PostService(IRepository<Post> PostRepository)
        {
            this.PostRepository = PostRepository;
        }

        public void DeletePost(int id)
        {
            Post Post = PostRepository.Get(id);
            PostRepository.Remove(Post);

        }

        public Post GetPost(int id)
        {
            return PostRepository.Get(id);
        }
        public IEnumerable<Post> GetPosts()
        {
            return PostRepository.GetAll();
        }

        public void InsertPost(Post Post)
        {
            PostRepository.Insert(Post);
        }

        public void UpdatePost(Post Post)
        {
            PostRepository.Update(Post);
        }
    }
}

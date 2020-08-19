using BD.DATA.Entity;
using System;
using System.Collections.Generic;

namespace DB.SERVICE
{
    public interface IPostService
    {
        IEnumerable<Post> GetPosts();
        Post GetPost(int id);
        void InsertPost(Post Post);
        void UpdatePost(Post Post);
        void DeletePost(int id);
    }
}

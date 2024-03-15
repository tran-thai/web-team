using WebTeam.Models;

namespace WebTeam.Services
{
    public interface ICommentsService
    {
        Task Add(Comment comment);
    }
}

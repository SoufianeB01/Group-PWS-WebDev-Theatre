using TheatreSystem.Models;


public class MatchingService
{
      public List<(User, User)> FindMatches(List<User> users)
    {
        var matches = new List<(User, User)>();

        // Compare each user with every other user
        for (int i = 0; i < users.Count; i++)
        {
            for (int j = i + 1; j < users.Count; j++)
            {
                var user1 = users[i];
                var user2 = users[j];

                // Check for shared movies
                var commonMovies = user1.WatchedMovies.Intersect(user2.WatchedMovies).ToList();
                if (commonMovies.Count > 0)
                {
                    matches.Add((user1, user2));
                }
            }
        }

        return matches;
    }
    

}
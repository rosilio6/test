using System.Net.Mail;
using AcceptanceTests.Data;


namespace AcceptanceTests.Bridge
{
    public interface IBridge
    {
        ///     <summary>
        ///     Connect rgisterd member to the system ==> uniq username and password are required.
        ///     </summary>
        ///     <param name="userName">Uniq user name </param> 
        ///     <param name="password">legal Password</param>   
        ///     <returns> 
        ///         In case of success returns 0.
        ///         In case of faliure returns 1.
        ///     </returns>
        int Login(string userName , string password);


        ///     <summary>
        ///     Rgister a new member to the system ==> uniq username uniq mail and password are required.
        ///     </summary>
        ///     <param name="userName">Uniq user name </param> 
        ///     <param name="password">legal Password</param>   
        ///     <param name="email">Uniq email address</param>   
        ///     <returns>
        ///        In case of success returns 0.
        ///        In case of faliure returns 1.
        ///     </returns>
        int Register(string userName, string password , MailAddress email);


        ///     <summary>
        ///     Kill user active season from the system.
        ///     </summary>
        ///     <param name="user">User instance</param> 
        ///     <returns> 
        ///         In case of success returns true.
        ///         In case of faliure returns false.
        ///     </returns>
        bool Logout(User user);


        ///     <summary>
        ///     Edit user porsonal information ==> username password mail avatar.
        ///     </summary>
        ///     <param name="user">The updated user instance</param>   
        ///     <returns>
        ///        The updated user after the changes applied.
        ///        In case of faliure returns null.
        ///     </returns>
        User EditUserDetails(User olduser, User newuser);


        ///     <summary>
        ///     Find registerd user by unique username.
        ///     </summary>
        ///     <param name="userName">The unique user name as a string</param>   
        ///     <returns>
        ///        Copy of the user instance.
        ///        In case of faliure returns null.
        ///     </returns>
        User TestfindUserByUserName(string userName);



        ///     <summary>
        ///     Create new game open for spectate and player==> only logged in user 
        ///     </summary>
        ///      <param name="byInPolicy">The amount of money that required to enter the table</param>   
        ///      <param name="numOfPlayers">The top barrier of player ==> (max people in table)</param>   
        ///     <returns>
        ///      In case of success returns true.
        ///      In case of faliure returns false.
        ///     </returns>
        bool CreateNewGame(int byInPolicy, int numOfPlayers);


    


        ///     <summary>
        ///     Create new game open only for players ==> only logged in user
        ///     </summary>
        ///      <param name="byInPolicy">The amount of money that required to enter the table</param>   
        ///      <param name="numOfPlayers">The top barrier of player ==> (max people in table)</param>   
        ///     <returns>
        ///      In case of success returns true.
        ///      In case of faliure returns false.
        ///     </returns>
        bool CreateNewGameOnlyForPlayers(int byInPolicy, int numOfPlayers);


        ///     <summary>
        ///     Join looged in user to existing game.
        ///     </summary>
        ///      <param name="game">Legal game with top barrier of players</param>   
        ///      <param name="user">The player</param>   
        ///      <param name="state">State of the player in the game ==> Spectator || Player </param>   
        ///     <returns>
        ///      In case of success returns true.
        ///      In case of faliure returns false.
        ///     </returns>
        bool JoinToGame(GameClient game, User user,State state);


        ///     <summary>
        ///     Remove player from active game.
        ///     </summary>
        ///      <param name="game">Legal game instance</param>   
        ///      <param name="user">Player inside a active game</param>   
        ///     <returns>
        ///      In case of success returns true.
        ///      In case of faliure returns false.
        ///     </returns>
        bool LeaveActiveGame(GameClient game, User user);

        ///     <summary>
        ///     Run replay of an inActive Game.
        ///     </summary>
        ///      <param name="game">Legal Inactive game instance</param>   
        ///     <returns>
        ///      In case of success returns true.
        ///      In case of faliure returns false.
        ///     </returns>
        bool ReplayInactiveGam(GameClient game);


        ///     <summary>
        ///     Save turn from replay of an inActive Game.
        ///     </summary>
        ///      <param name="userName">The name of the player</param>   
        ///      <param name="game">Legal Inactive game instance</param>   
        ///      <param name="turnNum">Index of the turn from the list of turns</param>   
        ///     <returns>
        ///      In case of success returns true.
        ///      In case of faliure returns false.
        ///     </returns>
        bool SaveTurnFromInactiveGame(string userName, GameClient game, int turnNum);


        ///     <summary>
        ///     Find list of all active games for specific state.
        ///     </summary>
        ///      <param name="state"> The state that the user wants to be in the game</param>   
        ///     <returns>
        ///      In case of success returns the number of active games (including zero games).
        ///      In case of faliure returns -1.
        ///     </returns>
        int FindAllActiveGames(State state);

        ///     <summary>
        ///     Add user (player or spectator ) to the first active game instance.
        ///     </summary>
        ///      <param name="user">  Logged in user instance</param>  
        ///      <param name="state"> The state that the user wants to be in the game</param>   
        ///     <returns>
        ///      In case of success returns true.
        ///      In case of faliure returns false.
        ///     </returns>
        bool JoinUserToFirstActiveGame(User user, State state);
    


        int FilterActiveGamesByUserName(string username);


        bool CreateNewGameFromInstance(GameClient game);
    }
}

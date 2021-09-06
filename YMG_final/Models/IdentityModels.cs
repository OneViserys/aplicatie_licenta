using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace YMG.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public byte[] ProfilePicture { get; set; }
        public virtual ICollection<Movie> Watchlist { get; set; }
        public virtual ICollection<Movie> Seen { get; set; }
        public virtual ICollection<Movie> Rated { get; set; }
        public virtual ICollection<Movie> FavouriteMovies { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Review> ReportedReviews { get; set; }
        public virtual ICollection<Comment> ReportedComments { get; set; }
        public virtual ICollection<Actor> FavouriteActors { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Genre> PreferredGenres { get; set; }




        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DbConnectionString", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(new Initp());
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReportReview> ReviewReports { get; set; }
        public DbSet<ReportComment> ReviewComments { get; set; }
        public DbSet<DiscussionForum> Forums { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ActorRole> ActorRoles { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class Initp : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            Genre horror = new Genre { Name = "Horror" };
            Genre thriller = new Genre { Name = "Thriller" };
            Genre scifi = new Genre { Name = "Science-Fiction" };
            Genre adventure = new Genre { Name = "Adventure" };
            Genre action = new Genre { Name = "Action" };
            Genre mistery = new Genre { Name = "Mistery" };
            Genre fantasy = new Genre { Name = "Fantasy" };
            Genre documentary = new Genre { Name = "Documentary" };
            Genre biography = new Genre { Name = "Biography" };
            Genre comedy = new Genre { Name = "Comedy" };
            Genre romance = new Genre { Name = "Romance" };
            Genre musical = new Genre { Name = "Musical" };
            Genre animation = new Genre { Name = "Animation" };
            Genre children = new Genre { Name = "Children Movie" };
            Genre crime = new Genre { Name = "Crime" };
            Genre drama = new Genre { Name = "Drama" };
            Genre historical = new Genre { Name = "Historical" };
            Genre spy = new Genre { Name = "Spy" };
            Genre western = new Genre { Name = "Western" };
            Genre dcomedy = new Genre { Name = "Dark Comedy" };
            Genre family = new Genre { Name = "Family" };
            Genre christmas = new Genre { Name = "Christmas" };
            Genre superhero = new Genre { Name = "Superhero" };
            context.Genres.Add(horror);
            context.Genres.Add(thriller);
            context.Genres.Add(scifi);
            context.Genres.Add(adventure);
            context.Genres.Add(action);
            context.Genres.Add(mistery);
            context.Genres.Add(fantasy);
            context.Genres.Add(documentary);
            context.Genres.Add(biography);
            context.Genres.Add(comedy);
            context.Genres.Add(romance);
            context.Genres.Add(musical);
            context.Genres.Add(animation);
            context.Genres.Add(children);
            context.Genres.Add(crime);
            context.Genres.Add(drama);
            context.Genres.Add(historical);
            context.Genres.Add(spy);
            context.Genres.Add(western);
            context.Genres.Add(dcomedy);
            context.Genres.Add(family);
            context.Genres.Add(christmas);
            context.Genres.Add(superhero);
            //fight club
            List<ActorRole> roles = new List<ActorRole>();
            Actor brad = new Actor { FullName = "Brad Pitt", CopyrightMessage = "© Glenn Francis, www.PacificProDigital.com", PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4c/Brad_Pitt_2019_by_Glenn_Francis.jpg/800px-Brad_Pitt_2019_by_Glenn_Francis.jpg", Bio = "An actor and producer known as much for his versatility as he is for his handsome face, Golden Globe-winner Brad Pitt's most widely recognized role may be Tyler Durden in Fight Club (1999). However, his portrayals of Billy Beane in Moneyball (2011), and Rusty Ryan in the remake of Ocean's Eleven (2001) and its sequels, also loom large in his filmography." };
            context.Actors.Add(brad);
            Actor edward = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/3/35/Ed_Norton_Shankbone_Metropolitan_Opera_2009.jpg", CopyrightMessage = "David Shankbone, CC BY 3.0 <https://creativecommons.org/licenses/by/3.0>, via Wikimedia Commons", FullName = "Edward Norton", Bio = "American actor, filmmaker and activist Edward Harrison Norton was born on August 18, 1969, in Boston, Massachusetts, and was raised in Columbia, Maryland." };
            context.Actors.Add(edward);
            Actor helena = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/6/6e/Helena_Bonham_Carter_2011_AA.jpg", CopyrightMessage = "David Torcivia, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Helena Bonham-Carter", Bio = "Helena Bonham Carter is an actress of great versatility, one of the UK's finest and most successful. \nBonham Carter was born May 26, 1966 in Golders Green, London, England, the youngest of three children of Elena(née Propper de Callejón), a psychotherapist, and Raymond Bonham Carter, a merchant banker." };
            context.Actors.Add(helena);
            Actor meat = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/0/01/Meat_Loaf_Photo_Op_GalaxyCon_Raleigh_2019.jpg", CopyrightMessage = "Super Festivals, CC BY 2.0 <https://creativecommons.org/licenses/by/2.0>, via Wikimedia Commons", FullName = "Meat Loaf", Bio = "Meat Loaf was born Marvin Lee Aday in Dallas, Texas, and moved to Los Angeles in 1967 to play in local bands. In 1970, he moved to New York and appeared in the Broadway musicals 'Hair', 'Rockabye Hamlet' and 'The Rocky Horror Show' and Off Broadway in 'Rainbow','More Than You Deserve, National Lampoon Show and the New York Shakespeare Festival's production of As You Like it and other productions at the famed New York Public Theatre. He made his film debut with a memorable role in the cult film The Rocky Horror Picture Show (1975)." };
            context.Actors.Add(meat);
            Actor jared = new Actor { CopyrightMessage = "Gage Skidmore from Peoria, AZ, United States of America, CC BY-SA 2.0 <https://creativecommons.org/licenses/by-sa/2.0>, via Wikimedia Commons", PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/0/00/Jared_Leto%2C_San_Diego_Comic_Con_2016_%282%29.jpg", FullName = "Jared Leto", Bio = "Jared Leto is a very familiar face in recent film history. Although he has always been the lead vocals, rhythm guitar, and songwriter for American band Thirty Seconds to Mars, Leto is an accomplished actor merited by the numerous, challenging projects he has taken in his life. He is known to be selective about his film roles." };
            context.Actors.Add(jared);
            List<Actor> fightClubCast = new List<Actor>
            {
                brad,
                edward,
                helena,
                meat,
                jared
            };
            List<Genre> fcGenres = new List<Genre>
            {
                action,
                thriller,
                drama,
                dcomedy
            };
            Movie fightCub = new Movie
            {
                Title = "Fight Club",
                Cast = fightClubCast,
                Year = 1999,
                Director = "David Fincher",
                Description = "A nameless first person narrator (Edward Norton) attends support groups in attempt to subdue his emotional state and relieve his insomniac state. When he meets Marla (Helena Bonham Carter), another fake attendee of support groups, his life seems to become a little more bearable. However when he associates himself with Tyler (Brad Pitt) he is dragged into an underground fight club and soap making scheme. Together the two men spiral out of control and engage in competitive rivalry for love and power. When the narrator is exposed to the hidden agenda of Tyler's fight club, he must accept the awful truth that Tyler may not be who he says he is.",
                Genres = fcGenres,
                TrailerUrl = "https://www.youtube.com/embed/qtRKdVHc-cE",
                CopyrightMessage = "",
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMmEzNTkxYjQtZTc0MC00YTVjLTg5ZTEtZWMwOWVlYzY0NWIwXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg",
                Released = true
            };
            ActorRole a = new ActorRole { CharacterName = "Tyler Durden", Actor = brad, ActorId = brad.ActorId, MovieId = fightCub.MovieId, Movie = fightCub };
            ActorRole b = new ActorRole { CharacterName = "Marla Singer", Actor = helena, ActorId = helena.ActorId, MovieId = fightCub.MovieId, Movie = fightCub };
            ActorRole c = new ActorRole { CharacterName = "Angel Face", Actor = jared, ActorId = jared.ActorId, MovieId = fightCub.MovieId, Movie = fightCub };
            ActorRole d = new ActorRole { CharacterName = "The Narrator", Actor = edward, ActorId = edward.ActorId, MovieId = fightCub.MovieId, Movie = fightCub };
            ActorRole e = new ActorRole { CharacterName = "Robert 'Bob' Paulsen", Actor = meat, ActorId = meat.ActorId, MovieId = fightCub.MovieId, Movie = fightCub };
            context.ActorRoles.Add(a);
            context.ActorRoles.Add(b);
            context.ActorRoles.Add(c);
            context.ActorRoles.Add(d);
            context.ActorRoles.Add(e);
            context.Movies.Add(fightCub);
            DiscussionForum forum = new DiscussionForum { Movie = fightCub };
            context.Forums.Add(forum);


            // hateful eight
            Actor kurt = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/d/db/Kurt_Russell_by_Gage_Skidmore_2.jpg", CopyrightMessage = "Gage Skidmore, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Kurt Russel", Bio = "Kurt Vogel Russell on March 17, 1951 in Springfield, Massachusetts to Louise Julia Russell (née Crone), a dancer & Bing Russell, an actor. He is of English, German, Scottish and Irish descent. His first roles were as a child on television series, including a lead role on the Western series The Travels of Jaimie McPheeters (1963). Russell landed a role in the Elvis Presley movie, It Happened at the World's Fair (1963), when he was eleven years old. Walt Disney himself signed Russell to a 10-year contract, and, according to Robert Osborne, he became the studio's top star of the 1970s." };
            context.Actors.Add(kurt);
            Actor samuel = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/3/38/Samuel_L_Jackson_at_San_Diego_ComicCon_2008.jpg", CopyrightMessage = "pinguino k, CC BY 2.0 <https://creativecommons.org/licenses/by/2.0>, via Wikimedia Commons", FullName = "Samuel L. Jackson", Bio = "Samuel L. Jackson is an American producer and highly prolific actor, having appeared in over 100 films, including Die Hard: With a Vengeance (1995), Unbreakable (2000), Shaft (2000), The 51st State (2001), Black Snake Moan (2006), Snakes on a Plane (2006), and the Star Wars prequel trilogy (1999-2005), as well as the Marvel Cinematic Universe." };
            context.Actors.Add(samuel);
            Actor walton = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/f/f9/Walton_Goggins_by_Gage_Skidmore.jpg", CopyrightMessage = "Gage Skidmore, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Walton Goggins", Bio = "Walton Goggins is an actor of considerable versatility and acclaim who has delivered provocative performances in a multitude of feature films and television series. He won a Critics' Choice Award for his performance in the HBO comedy series 'Vice Principals' and landed an Emmy nomination for his role of Boyd Crowder on FX's 'Justified', among numerous accolades." };
            context.Actors.Add(walton);
            Actor tim = new Actor { CopyrightMessage = "Gage Skidmore, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/3/36/Tim_Roth_by_Gage_Skidmore_2.jpg", FullName = "Tim Roth", Bio = "Often mistaken for an American because of his skill at imitating accents, actor Tim Roth was born Timothy Simon Roth on May 14, 1961 in Lambeth, London, England. His mother, Ann, was a teacher and landscape painter. His father, Ernie, was a journalist who had changed the family name from Smith to Roth; Ernie was born in Brooklyn, New York, to an immigrant family of Irish ancestry." };
            context.Actors.Add(tim);
            Actor jenniferLeigh = new Actor { CopyrightMessage = "Gage Skidmore, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2e/Jennifer_Jason_Leigh_by_Gage_Skidmore.jpg", FullName = "Jennifer Jason Leigh", Bio = "Jennifer Jason Leigh was born Jennifer Lee Morrow in Los Angeles, California, the daughter of writer Barbara Turner and actor Vic Morrow. Her father was of Russian Jewish descent and her mother was of Austrian Jewish ancestry. She is the sister of Carrie Ann Morrow and half-sister of actress Mina Badie." };
            context.Actors.Add(jenniferLeigh);
            Actor channing = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/7/74/Channing_Tatum_%2819421648739%29.jpg", CopyrightMessage = "Gordon  Correll, CC BY-SA 2.0 <https://creativecommons.org/licenses/by-sa/2.0>, via Wikimedia Commons", FullName = "Channing Tatum", Bio = "Channing Tatum was born in a small town, Cullman, Alabama, 50 miles north of Birmingham. He is the son of Kay (Faust), an airline worker, and Glenn Matthew Tatum, who worked in construction. Growing up, he was full of energy and somewhat troublesome, so his parents decided to enroll him in different sports such as track and field, baseball, soccer, and football to keep him out of trouble. In the ninth grade he was sent to Catholic school. It was there that he discovered his passion for football and his hopes became centered on earning an athletic college scholarship." };
            context.Actors.Add(channing);
            Actor michaelMadsen = new Actor { CopyrightMessage = "Gage Skidmore, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b9/Michael_Madsen_by_Gage_Skidmore.jpg", FullName = "Michael Madsen", Bio = "Michael Madsen's long career spans nearly 40 years and more than 170 films in which he has played memorable characters in myriad box office hits, including: Kill Bill: Vol. 1 (2003), Kill Bill: Vol. 2 (2004) Sin City (2005), Hell Ride (2008), Die Another Day (2002), Donnie Brasco (1997), Species (1995), The Getaway (1994), Thelma & Louise (1991), and Free Willy (1993). Michael is notably recognized for his role as Mr. Blonde, in Quentin Tarantino's Reservoir Dogs (1992)." };
            context.Actors.Add(michaelMadsen);
            List<Actor> hatefulEightCast = new List<Actor>
            {
                kurt,
                samuel,
                walton,
                tim,
                jenniferLeigh,
                channing,
                michaelMadsen
            };
            List<Genre> hatefulGenres = new List<Genre>
            {
                western,
                thriller,
                drama
            };
            Movie hatefulEight = new Movie
            { CopyrightMessage = "© 2015 The Weinstein Co.", PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjA1MTc1NTg5NV5BMl5BanBnXkFtZTgwOTM2MDEzNzE@._V1_.jpg", Title = "The Hateful Eight", Director = "Quentin Tarantino", Year = 2016, TrailerUrl = "https://www.youtube.com/embed/trqB1pYN4fk" };
            hatefulEight.Description = "The Hateful Eight (sometimes marketed as The H8ful Eight) is a 2015 American revisionist western thriller film written and directed by Quentin Tarantino. It stars Samuel L. Jackson, Kurt Russell, Jennifer Jason Leigh, Walton Goggins, Demián Bichir, Tim Roth, Michael Madsen, and Bruce Dern as eight strangers who seek refuge from a blizzard in a stagecoach stopover some time after the American Civil War.";
            hatefulEight.Cast = hatefulEightCast;
            hatefulEight.Genres = hatefulGenres;
            hatefulEight.Released = true;
            a = new ActorRole { CharacterName = "Daisy Domergue", Actor = jenniferLeigh, ActorId = jenniferLeigh.ActorId, MovieId = hatefulEight.MovieId, Movie = hatefulEight };
            b = new ActorRole { CharacterName = "Major Marquis Warren", Actor = samuel, ActorId = samuel.ActorId, MovieId = hatefulEight.MovieId, Movie = hatefulEight };
            c = new ActorRole { CharacterName = "John Ruth", Actor = kurt, ActorId = kurt.ActorId, MovieId = hatefulEight.MovieId, Movie = hatefulEight };
            d = new ActorRole { CharacterName = "Oswaldo Mobray", Actor = tim, ActorId = tim.ActorId, MovieId = hatefulEight.MovieId, Movie = hatefulEight };
            e = new ActorRole { CharacterName = "Joe Gage", Actor = michaelMadsen, ActorId = michaelMadsen.ActorId, MovieId = hatefulEight.MovieId, Movie = hatefulEight };
            ActorRole f = new ActorRole { CharacterName = "Sheriff Chris Minx", Actor = walton, ActorId = walton.ActorId, MovieId = hatefulEight.MovieId, Movie = hatefulEight };
            ActorRole g = new ActorRole { CharacterName = "Jody", Actor = channing, ActorId = channing.ActorId, MovieId = hatefulEight.MovieId, Movie = hatefulEight };

            context.ActorRoles.Add(a);
            context.ActorRoles.Add(b);
            context.ActorRoles.Add(c);
            context.ActorRoles.Add(d);
            context.ActorRoles.Add(e);
            context.ActorRoles.Add(f);
            context.ActorRoles.Add(g);
            context.Movies.Add(hatefulEight);
            forum = new DiscussionForum { Movie = hatefulEight };
            context.Forums.Add(forum);


            // Django - walton gogg
            Actor leo = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/f/f9/Leonardo_DiCaprio.jpeg", CopyrightMessage = "Falkenauge, CC BY-SA 3.0 <http://creativecommons.org/licenses/by-sa/3.0/>, via Wikimedia Commons", FullName = "Leonardo DiCaprio", Bio = "Few actors in the world have had a career quite as diverse as Leonardo DiCaprio's. DiCaprio has gone from relatively humble beginnings, as a supporting cast member of the sitcom Growing Pains (1985) and low budget horror movies, such as Critters 3 (1991), to a major teenage heartthrob in the 1990s, as the hunky lead actor in movies such as Romeo + Juliet (1996) and Titanic (1997), to then become a leading man in Hollywood blockbusters, made by internationally renowned directors such as Martin Scorsese and Christopher Nolan." };
            context.Actors.Add(leo);
            Actor jamie = new Actor { CopyrightMessage = "Ed Edahl, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/8/84/Jamie_Foxx_face.jpg", FullName = "Jamie Foxx", Bio = "Jamie Foxx is an American actor, singer and comedian. He won an Academy Award for Best Actor, BAFTA Award for Best Actor in a Leading Role, and Golden Globe Award for Best Actor in a Musical or Comedy, for his work in the biographical film Ray (2004). The same year, he was nominated for the Academy Award for Best Supporting Actor for his role in the action film Collateral (2004). Other prominent acting roles include the title role in the film Django Unchained (2012), the supervillain Electro in The Amazing Spider-Man 2 (2014), and William Stacks in the modern version of Annie (2014)." };
            context.Actors.Add(jamie);
            Actor cate = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/e/e6/2018-05-19-_Cannes-Cate_Blanchett-5591_%2827924434047%29.jpg", CopyrightMessage = "Joan Hernandez Mir, CC BY 2.0 <https://creativecommons.org/licenses/by/2.0>, via Wikimedia Commons", FullName = "Cate Blanchett", Bio = "Cate Blanchett was born on May 14, 1969 in Melbourne, Victoria, Australia, to June (Gamble), an Australian teacher and property developer, and Robert DeWitt Blanchett, Jr., an American advertising executive, originally from Texas. She has an older brother and a younger sister. When she was ten years old, her 40-year-old father died of a sudden heart attack. Her mother never remarried, and her grandmother moved in to help her mother." };
            context.Actors.Add(cate);
            Actor christoph = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/4/47/Christoph_Waltz_Cannes_2013.jpg", CopyrightMessage = "Georges Biard, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Christoph Waltz", Bio = "Christoph Waltz is an Austrian-German actor. He is known for his work with American filmmaker Quentin Tarantino, receiving acclaim for portraying SS-Standartenführer Hans Landa in Inglourious Basterds (2009) and bounty hunter Dr. King Schultz in Django Unchained (2012). For each performance, he won an Academy Award, a BAFTA Award, and a Golden Globe Award for Best Supporting Actor. Additionally, he received the Best Actor Award at the Cannes Film Festival and a Screen Actors Guild Award for his portrayal of Landa." };
            context.Actors.Add(christoph);
            Actor jonah = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2e/Jonah_Hill_-_001.jpg", CopyrightMessage = "", FullName = "Jonah Hill", Bio = "Jonah Hill was born and raised in Los Angeles, the son of Sharon Feldstein (née Chalkin), a fashion designer and costume stylist, and Richard Feldstein, a tour accountant for Guns N' Roses. He is the brother of music manager Jordan Feldstein and actress Beanie Feldstein. He graduated from Crossroads School in Santa Monica and went on to The New School in New York to study drama." };
            context.Actors.Add(jonah);
            List<Actor> djangoCast = new List<Actor>
            {
                walton,
                leo,
                jamie,
                christoph,
                jonah,
                samuel
            };
            List<Genre> djangoGenres = new List<Genre>
            {
                western,
                action
            };
            Movie djangoUnchained = new Movie
            { PosterUrl = "https://upload.wikimedia.org/wikipedia/en/8/8b/Django_Unchained_Poster.jpg", CopyrightMessage = "© 2015 The Weinstein Co.", Title = "Django Unchained", TrailerUrl = "https://www.youtube.com/embed/0fUCuvNlOCg", Year = 2013, Director = "Quentin Tarantino", Cast = djangoCast, Genres = djangoGenres, Description = "Django Unchained initially represents itself as the sole story of Django and his “unchaining,” or break from bondage. The opening credits present a downtrodden slave marching forward along to someone else’s accord, yet the roaring, almost inciting music in the background argues that the hero “Django” is underneath that individual.", Released = true };
            a = new ActorRole { CharacterName = "Django", Actor = jamie, ActorId = jamie.ActorId, MovieId = djangoUnchained.MovieId, Movie = djangoUnchained };
            b = new ActorRole { CharacterName = "Dr. King Schultz", Actor = christoph, ActorId = christoph.ActorId, MovieId = djangoUnchained.MovieId, Movie = djangoUnchained };
            c = new ActorRole { CharacterName = "Calvin Candie", Actor = leo, ActorId = leo.ActorId, MovieId = djangoUnchained.MovieId, Movie = djangoUnchained };
            d = new ActorRole { CharacterName = "Stephen", Actor = samuel, ActorId = samuel.ActorId, MovieId = djangoUnchained.MovieId, Movie = djangoUnchained };
            e = new ActorRole { CharacterName = "Billy Crash", Actor = walton, ActorId = walton.ActorId, MovieId = djangoUnchained.MovieId, Movie = djangoUnchained };
            f = new ActorRole { CharacterName = "Bag Head #2", Actor = jonah, ActorId = jonah.ActorId, MovieId = djangoUnchained.MovieId, Movie = djangoUnchained };

            context.ActorRoles.Add(a);
            context.ActorRoles.Add(b);
            context.ActorRoles.Add(c);
            context.ActorRoles.Add(d);
            context.ActorRoles.Add(e);
            context.ActorRoles.Add(f);
            context.Movies.Add(djangoUnchained);
            forum = new DiscussionForum { Movie = djangoUnchained };
            context.Forums.Add(forum);


            // the wolf of wall street (jonah hill, leo) - comedy, drama, biography
            Actor margot = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/9/98/Margot_Robbie_at_Somerset_House_in_2013_%28cropped%29.jpg", CopyrightMessage = "https://www.flickr.com/photos/drlovell, CC BY-SA 2.0 <https://creativecommons.org/licenses/by-sa/2.0>, via Wikimedia Commons", FullName = "Margot Robbie", Bio = "Margot Elise Robbie was born on July 2, 1990 in Dalby, Queensland, Australia to Scottish parents. Her mother, Sarie Kessler, is a physiotherapist, and her father, is Doug Robbie. She comes from a family of four children, having two brothers and one sister. She graduated from Somerset College in Mudgeeraba, Queensland, Australia, a suburb in the Gold Coast hinterland of South East Queensland, where she and her siblings were raised by their mother and spent much of her time at the farm belonging to her grandparents. In her late teens, she moved to Melbourne, Victoria, Australia to pursue an acting career." };
            context.Actors.Add(margot);
            Actor matthew = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/3/3b/Matthew_McConaughey_2011.jpg", CopyrightMessage = "David Torcivia, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Matthew McConaughey", Bio = "American actor and producer Matthew David McConaughey was born in Uvalde, Texas. His mother, Mary Kathleen (McCabe), is a substitute school teacher originally from New Jersey. His father, James Donald McConaughey, was a Mississippi-born gas station owner who ran an oil pipe supply business. He is of Irish, Scottish, English, German, and Swedish descent." };
            context.Actors.Add(matthew);
            List<Actor> wowsCast = new List<Actor>
            {
                leo,
                margot,
                matthew,
                jonah
            };
            List<Genre> wowGeneres = new List<Genre>
            {
                comedy,
                dcomedy,
                drama,
                crime
            };
            Movie wolfOfWallStreet = new Movie
            {
                PosterUrl = "https://upload.wikimedia.org/wikipedia/en/d/d8/The_Wolf_of_Wall_Street_%282013%29.png",
                CopyrightMessage = "",
                Title = "The Wolf of Wall Street",
                Year = 2013,
                TrailerUrl = "https://www.youtube.com/embed/iszwuX1AK6A",
                Director = "Martin Scorsese",
                Genres = wowGeneres,
                Cast = wowsCast,
                Released = true,
                Description = "The Wolf of Wall Street begins in sunny 1987. Jordan Belfort (Leonardo DiCaprio) is a Wall Street stockbroker for L.F. Rothschild. His boss, Mark Hanna (Matthew McConaughey), introduces him to the “Greed is Good” stockbroker culture of the day rife with easy drugs, easy women, and easy money."
            };
            a = new ActorRole { CharacterName = "Jordan Belfort", Actor = leo, ActorId = leo.ActorId, MovieId = wolfOfWallStreet.MovieId, Movie = wolfOfWallStreet };
            b = new ActorRole { CharacterName = "Donnie Azoff", Actor = jonah, ActorId = jonah.ActorId, MovieId = wolfOfWallStreet.MovieId, Movie = wolfOfWallStreet };
            c = new ActorRole { CharacterName = "Naomi Lapaglia", Actor = margot, ActorId = margot.ActorId, MovieId = wolfOfWallStreet.MovieId, Movie = wolfOfWallStreet };
            d = new ActorRole { CharacterName = "Mark Hanna", Actor = matthew, ActorId = matthew.ActorId, MovieId = wolfOfWallStreet.MovieId, Movie = wolfOfWallStreet };

            context.ActorRoles.Add(a);
            context.ActorRoles.Add(b);
            context.ActorRoles.Add(c);
            context.ActorRoles.Add(d);
            context.Movies.Add(wolfOfWallStreet);
            forum = new DiscussionForum { Movie = wolfOfWallStreet };
            context.Forums.Add(forum);

            // Zombieland - comedy
            Actor woody = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/57/Woody_Harrelson_October_2016.jpg/615px-Woody_Harrelson_October_2016.jpg", CopyrightMessage = "", FullName = "Woody Harrelson", Bio = "Academy Award-nominated and Emmy Award-winning actor Woodrow Tracy Harrelson was born on July 23, 1961 in Midland, Texas, to Diane Lou (Oswald) and Charles Harrelson. He grew up in Lebanon, Ohio, where his mother was from. After receiving degrees in theater arts and English from Hanover College, he had a brief stint in New York theater. He was soon cast as Woody on TV series Cheers (1982), which wound up being one of the most-popular TV shows ever and also earned Harrelson an Emmy for his performance in 1989." };
            context.Actors.Add(woody);
            Actor jesse = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/3/37/Jesse_Eisenberg_by_Gage_Skidmore.jpg", CopyrightMessage = "Gage Skidmore, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Jesse Eisenberg", Bio = "Curly haired and with a fast-talking voice, Jesse Eisenberg is a movie actor, known for his Academy Award nominated role as Mark Zuckerberg in the 2010 film The Social Network. He has also starred in the films The Squid and the Whale, Adventureland, The Education of Charlie Banks, 30 Minutes or Less, Now You See Me and Zombieland. Additionally, he played Lex Luthor in the 2016 film Batman v Superman: Dawn of Justice." };
            context.Actors.Add(jesse);
            Actor emma = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/9/94/Emma_Stone_by_Gage_Skidmore.jpg", CopyrightMessage = "Gage Skidmore, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Emma Stone", Bio = "Emily Jean 'Emma' Stone was born in Scottsdale, Arizona, to Krista (Yeager), a homemaker, and Jeffrey Charles Stone, a contracting company founder and CEO. She is of Swedish, German, and British Isles descent. Stone began acting as a child as a member of the Valley Youth Theatre in Phoenix, Arizona, where she made her stage debut in a production of Kenneth Grahame's 'The Wind in the Willows'. She appeared in many more productions through her early teens until, at the age of fifteen, she decided that she wanted to make acting her career." };
            context.Actors.Add(emma);
            Actor bill = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b3/Bill_Murray_2010_%28cropped%29.jpg", CopyrightMessage = "David Shankbone, CC BY 2.0 <https://creativecommons.org/licenses/by/2.0>, via Wikimedia Commons", FullName = "Bill Murray", Bio = "Bill Murray is an American actor, comedian, and writer. The fifth of nine children, he was born William James Murray in Wilmette, Illinois, to Lucille (Collins), a mailroom clerk, and Edward Joseph Murray II, who sold lumber. He is of Irish descent. Among his siblings are actors Brian Doyle-Murray, Joel Murray, and John Murray. He and most of his siblings worked as caddies, which paid his tuition to Loyola Academy, a Jesuit school. He played sports and did some acting while in that school, but in his words, mostly 'screwed off'." };
            context.Actors.Add(bill);
            List<Actor> zombielandCast = new List<Actor>
            {
                woody,
                jesse,
                emma,
                bill
            };
            List<Genre> zombieGenres = new List<Genre>
            {
                comedy,
                horror,
                action
            };
            Movie zombieland = new Movie
            {
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTU5MDg0NTQ1N15BMl5BanBnXkFtZTcwMjA4Mjg3Mg@@._V1_.jpg",
                CopyrightMessage = "",
                Title = "Zombieland",
                Year = 2009,
                Director = "Ruben Fleischer",
                Cast = zombielandCast,
                TrailerUrl = "https://www.youtube.com/embed/ZlW9yhUKlkQ",
                Genres = zombieGenres,
                Released = true,
                Description = "Zombieland is a 2009 American post-apocalyptic zombie comedy film directed by Ruben Fleischer in his theatrical debut and written by Rhett Reese and Paul Wernick. The film stars Woody Harrelson, Jesse Eisenberg, Emma Stone, and Abigail Breslin as survivors of a zombie apocalypse."
            };
            a = new ActorRole { CharacterName = "Columbus", Actor = jesse, ActorId = jesse.ActorId, MovieId = zombieland.MovieId, Movie = zombieland };
            b = new ActorRole { CharacterName = "Tallahassee", Actor = woody, ActorId = woody.ActorId, MovieId = zombieland.MovieId, Movie = zombieland };
            c = new ActorRole { CharacterName = "Wichita", Actor = emma, ActorId = emma.ActorId, MovieId = zombieland.MovieId, Movie = zombieland };
            d = new ActorRole { CharacterName = "Bill Murray", Actor = bill, ActorId = bill.ActorId, MovieId = zombieland.MovieId, Movie = zombieland };

            context.ActorRoles.Add(a);
            context.ActorRoles.Add(b);
            context.ActorRoles.Add(c);
            context.ActorRoles.Add(d);
            context.Movies.Add(zombieland);
            forum = new DiscussionForum { Movie = zombieland };
            context.Forums.Add(forum);


            // The Hunger Games - woody h.
            Actor jenniferLawrence = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/0/0b/Jennifer_Lawrence_SDCC_2015_X-Men.jpg", CopyrightMessage = "Gage Skidmore, CC BY-SA 4.0 <https://creativecommons.org/licenses/by-sa/4.0>, via Wikimedia Commons", FullName = "Jennifer Lawrence", Bio = "As the highest-paid actress in the world in 2015 and 2016, and with her films grossing over $5.5 billion worldwide, Jennifer Lawrence is often cited as the most successful actress of her generation. She is also thus far the only person born in the 1990s to have won an acting Oscar." };
            context.Actors.Add(jenniferLawrence);
            Actor donald = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/1/1d/Donald_Sutherland_%28cropped%29.JPG", CopyrightMessage = "Festival TV Monte-Carlo, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Donald Sutherland", Bio = "The towering presence of Canadian actor Donald Sutherland is often noticed, as are his legendary contributions to cinema. He has appeared in almost 200 different shows and films. He is also the father of renowned actor Kiefer Sutherland, among others." };
            context.Actors.Add(donald);
            Actor lenny = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/2/28/Lenny_Kravitz_2011.jpg", CopyrightMessage = "Liseberg, CC BY 3.0 <https://creativecommons.org/licenses/by/3.0>, via Wikimedia Commons", FullName = "Lenny Kravitz", Bio = "Four time Grammy Award-winner Leonard Albert Kravitz was born on May 26, 1964 in Manhattan, New York City, New York, the only child of actress Roxie Roker and Hollywood producer Sy Kravitz. The family moved to California when Lenny was ten. His father was Jewish and his mother was of African-American and Afro-Bahamian ancestry." };
            context.Actors.Add(lenny);
            Actor liam = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/4/4b/Hemsworth_Liam.jpg", CopyrightMessage = "Soof 007, CC BY-SA 4.0 <https://creativecommons.org/licenses/by-sa/4.0>, via Wikimedia Commons", FullName = "Liam Hemsworth", Bio = "Liam Hemsworth was born on January 13, 1990, in Melbourne, Australia, and is the younger brother of actors Chris Hemsworth and Luke Hemsworth. He is the son of Leonie (van Os), a teacher of English, and Craig Hemsworth, a social-services counselor. He is of Dutch (from his immigrant maternal grandfather), Irish, English, Scottish, and German ancestry." };
            context.Actors.Add(liam);
            Actor elizabeth = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/e/ee/Elizabeth_Banks_by_Gage_Skidmore.jpg", CopyrightMessage = "https://upload.wikimedia.org/wikipedia/commons/4/4b/Hemsworth_Liam.jpg", FullName = "Elizabeth Banks", Bio = "Elizabeth Banks was born Elizabeth Mitchell in Pittsfield, a small city in the Berkshires in western Massachusetts near the New York border, on February 10, 1974. She is the daughter of Ann (Wallace), who worked in a bank, and Mark P. Mitchell, a factory worker. Elizabeth describes herself as having been seen as a 'goody two - shoes' in her youth who was nominated for the local Harvest Queen." };
            context.Actors.Add(elizabeth);
            Actor joshHutcherson = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/d/d5/Josh_Hutcherson_by_Gage_Skidmore.jpg", CopyrightMessage = "Gage Skidmore, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Josh Hutcherson", Bio = "Joshua Ryan Hutcherson was born on October 12, 1992 in Union, Kentucky to Michelle Fightmaster, who worked for Delta Air Lines, and Chris Hutcherson, an EPA analyst. He has one younger brother, Connor Hutcherson. From the age of four, Josh knew that he wanted to be an actor. In order to pursue his goal, Josh and his family moved to Los Angeles when he was nine-years-old." };
            context.Actors.Add(joshHutcherson);
            List<Actor> hgCast = new List<Actor>
            {
                jenniferLawrence,
                joshHutcherson,
                woody,
                donald,
                liam,
                elizabeth,
                lenny
            };
            List<Genre> hgGenres = new List<Genre>
            {
                thriller,
                drama,
                adventure,
                scifi
            };
            Movie hungerGames = new Movie
            {
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjA4NDg3NzYxMF5BMl5BanBnXkFtZTcwNTgyNzkyNw@@._V1_.jpg",
                CopyrightMessage = "",
                Title = "The Hunger Games",
                Year = 2012,
                Director = "Francis Lawrence",
                Cast = hgCast,
                TrailerUrl = "https://www.youtube.com/embed/GWU-xLViib0",
                Genres = hgGenres,
                Released = true,
                Description = "In a dystopian future, the totalitarian nation of Panem is divided into 12 districts and the Capitol. Each year two young representatives from each district are selected by lottery to participate in The Hunger Games. Part entertainment, part brutal retribution for a past rebellion, the televised games are broadcast throughout Panem."
            };
            a = new ActorRole { CharacterName = "Katniss Everdeen", Actor = jenniferLawrence, ActorId = jenniferLawrence.ActorId, MovieId = hungerGames.MovieId, Movie = hungerGames };
            b = new ActorRole { CharacterName = "Peeta Mellark", Actor = joshHutcherson, ActorId = joshHutcherson.ActorId, MovieId = hungerGames.MovieId, Movie = hungerGames };
            c = new ActorRole { CharacterName = "Haymitch Abernathy", Actor = woody, ActorId = woody.ActorId, MovieId = hungerGames.MovieId, Movie = hungerGames };
            d = new ActorRole { CharacterName = "Gale Hawthorne", Actor = liam, ActorId = liam.ActorId, MovieId = hungerGames.MovieId, Movie = hungerGames };
            e = new ActorRole { CharacterName = "President Coriolanus Snow", Actor = donald, ActorId = donald.ActorId, MovieId = hungerGames.MovieId, Movie = hungerGames };
            f = new ActorRole { CharacterName = "Effie Trinket", Actor = elizabeth, ActorId = elizabeth.ActorId, MovieId = hungerGames.MovieId, Movie = hungerGames };
            g = new ActorRole { CharacterName = "Cinna", Actor = lenny, ActorId = lenny.ActorId, MovieId = hungerGames.MovieId, Movie = hungerGames };

            context.ActorRoles.Add(a);
            context.ActorRoles.Add(b);
            context.ActorRoles.Add(c);
            context.ActorRoles.Add(d);
            context.ActorRoles.Add(e);
            context.ActorRoles.Add(f);
            context.ActorRoles.Add(g);
            context.Movies.Add(hungerGames);
            forum = new DiscussionForum { Movie = hungerGames };
            context.Forums.Add(forum);


            // How the Grinch stole christmas
            Actor jim = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/8/8b/Jim_Carrey_2008.jpg", CopyrightMessage = "Ian Smith from London, England, CC BY-SA 2.0 <https://creativecommons.org/licenses/by-sa/2.0>, via Wikimedia Commons", FullName = "Jim Carrey", Bio = "Jim Carrey, Canadian-born and a U.S. citizen since 2004, is an actor and producer famous for his rubbery body movements and flexible facial expressions. The two-time Golden Globe-winner rose to fame as a cast member of the Fox sketch comedy In Living Color (1990) but leading roles in Ace Ventura: Pet Detective (1994), Dumb and Dumber (1994) and The Mask (1994) established him as a bankable comedy actor." };
            context.Actors.Add(jim);
            Actor christine = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/c/cb/ROMY_2012_04_Christine_Baranski_crop.JPG", CopyrightMessage = "Manfred Werner (Tsui), CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Christine Baranski", Bio = "Christine Baranski is an American actress from Buffalo, New York. She has had a relatively lengthy career in both film and television. She has been nominated for 15 Emmy Awards, winning once. One of her most popular roles was that of neuroscientist Dr. Beverly Hofstadter in the sitcom The Big Bang Theory. She played this role from 2009 to 2019." };
            context.Actors.Add(christine);
            Actor taylor = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/0/05/Taylor_Momsen_at_Met_Opera_%283100919959%29.jpg", CopyrightMessage = "https://www.flickr.com/photos/rubenstein_/, CC BY 2.0 <https://creativecommons.org/licenses/by/2.0>, via Wikimedia Commons", FullName = "Taylor Momsen", Bio = "Taylor Momsen was born on July 26, 1993 in St. Louis, Missouri, USA as Taylor Michel Momsen. She is an actress, known for How the Grinch Stole Christmas (2000), Spy Kids 2: Island of Lost Dreams (2002) and Underdog (2007)." };
            context.Actors.Add(taylor);
            List<Actor> grinchCast = new List<Actor>
            {
                jim,
                christine,
                taylor
            };
            List<Genre> grinchGenres = new List<Genre>
            {
                christmas,
                family,
                comedy
            };
            Movie grinch = new Movie
            {
                PosterUrl = "https://upload.wikimedia.org/wikipedia/en/e/e7/How_the_Grinch_Stole_Christmas_film_poster.jpg",
                CopyrightMessage = "",
                Title = "How the Grinch stole Christmas",
                Director = "Ron Howard",
                Year = 2000,
                Cast = grinchCast,
                Genres = grinchGenres,
                TrailerUrl = "https://www.youtube.com/embed/YQV5Pr7pWtM",
                Released = true,
                Description = "On the outskirts of Whoville lives a green, revenge-seeking Grinch who plans to ruin Christmas for all of the citizens of the town. Inside a snowflake exists the magical land of Whoville, wherein live the Whos, an almost-mutated sort of Munchkin-like people who all love Christmas."
            };
            a = new ActorRole { CharacterName = "The Grinch", Actor = jim, ActorId = jim.ActorId, MovieId = grinch.MovieId, Movie = grinch };
            b = new ActorRole { CharacterName = "Cindy Lou Who", Actor = taylor, ActorId = taylor.ActorId, MovieId = grinch.MovieId, Movie = grinch };
            c = new ActorRole { CharacterName = "Martha May Whovier", Actor = christine, ActorId = christine.ActorId, MovieId = grinch.MovieId, Movie = grinch };

            context.ActorRoles.Add(a);
            context.ActorRoles.Add(b);
            context.ActorRoles.Add(c);
            context.Movies.Add(grinch);
            forum = new DiscussionForum { Movie = grinch };
            context.Forums.Add(forum);


            // catching fire
            Actor sam = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/8/82/Sam_Claflin_2014.jpg", CopyrightMessage = "Ibsan73, CC BY 2.0 <https://creativecommons.org/licenses/by/2.0>, via Wikimedia Commons", FullName = "Sam Claflin", Bio = "English actor Sam Claflin was born in Ipswich, England, to Susan A. (Clarke), a classroom assistant, and Mark J. Claflin, a finance officer. As a child, he was football-mad, often going to see his favorite team, Norwich City. He was a talented footballer, playing for Norwich schools at city level and Norfolk county level. However, he suffered two broken ankles and at 16 gave up thinking about a footballing career. He took up performing arts and a teacher from Costessey High School was impressed with his performance in a school play, and encouraged him to take up drama." };
            context.Actors.Add(sam);
            Actor jena = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/1/13/Jena_Malone_Deauville.jpg", CopyrightMessage = "Georges Biard, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Jena Malone", Bio = "Jena Malone was born in Sparks, Nevada, to Deborah Malone and Edward Berge. Her grandfather owned a casino, Karl's Silver Club, in Sparks. She was raised by her mother and her mother's partner. Beginning as a child actress, and then stepping up to roles as a young adult, Malone's career path has been compared to that of Jodie Foster, herself a former child actress and who has co-starred with Malone in two movies. Jena is often described as having a maturity beyond her years and, in her career thus far, she has often tackled roles that are difficult and are not standard fare for actors her age." };
            context.Actors.Add(jena);
            Actor jeffrey = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/9/97/Jeffrey_Wright_by_Gage_Skidmore.jpg", CopyrightMessage = "Gage Skidmore, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Jeffrey Wright", Bio = "Born and raised in Washington DC, Jeffrey Wright graduated from Amherst College in 1987. Although he studied Political Science while at Amherst, Wright left the school with a love for acting. Shortly after graduating he won an acting scholarship to NYU, but dropped out after only two months to pursue acting full-time. With roles in Presumed Innocent (1990), and the Broadway production of Angels in America, (in which he won a Tony award), within a relatively short time Wright was able to show off his exceptional talent and ability on both stage and screen alike." };
            context.Actors.Add(jeffrey);
            List<Actor> cfCast = new List<Actor>
            {
                jenniferLawrence,
                joshHutcherson,
                woody,
                donald,
                sam,
                jena,
                elizabeth,
                lenny,
                liam,
                jeffrey
            };
            List<Genre> cfGenres = new List<Genre>
            {
                scifi,
                action,
                adventure,
                thriller
            };
            Movie catchingFire = new Movie
            {
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTAyMjQ3OTAxMzNeQTJeQWpwZ15BbWU4MDU0NzA1MzAx._V1_.jpg",
                CopyrightMessage = "",
                Title = "The Hunger Games: Catching Fire",
                Year = 2013,
                Director = "Francis Lawrence",
                Cast = cfCast,
                TrailerUrl = "https://www.youtube.com/embed/EAzGXqJSDJ8",
                Genres = cfGenres,
                Released = true,
                Description = "The plot of Catching Fire begins a few months after the previous installment; Katniss Everdeen and fellow District 12 tribute Peeta Mellark have returned home safely after winning the 74th Annual Hunger Games. Throughout the story, Katniss senses that a rebellion against the oppressive Capitol is simmering throughout the districts."
            };
            a = new ActorRole { CharacterName = "Katniss Everdeen", Actor = jenniferLawrence, ActorId = jenniferLawrence.ActorId, MovieId = catchingFire.MovieId, Movie = catchingFire };
            b = new ActorRole { CharacterName = "Peeta Mellark", Actor = joshHutcherson, ActorId = joshHutcherson.ActorId, MovieId = catchingFire.MovieId, Movie = catchingFire };
            c = new ActorRole { CharacterName = "Haymitch Abernathy", Actor = woody, ActorId = woody.ActorId, MovieId = catchingFire.MovieId, Movie = catchingFire };
            d = new ActorRole { CharacterName = "Gale Hawthorne", Actor = liam, ActorId = liam.ActorId, MovieId = catchingFire.MovieId, Movie = catchingFire };
            e = new ActorRole { CharacterName = "President Coriolanus Snow", Actor = donald, ActorId = donald.ActorId, MovieId = catchingFire.MovieId, Movie = catchingFire };
            f = new ActorRole { CharacterName = "Effie Trinket", Actor = elizabeth, ActorId = elizabeth.ActorId, MovieId = catchingFire.MovieId, Movie = catchingFire };
            g = new ActorRole { CharacterName = "Cinna", Actor = lenny, ActorId = lenny.ActorId, MovieId = catchingFire.MovieId, Movie = catchingFire };
            ActorRole h = new ActorRole { CharacterName = "Finnick Odair", Actor = sam, ActorId = sam.ActorId, MovieId = catchingFire.MovieId, Movie = catchingFire };
            ActorRole i = new ActorRole { CharacterName = "Johanna Mason", Actor = jena, ActorId = jena.ActorId, MovieId = catchingFire.MovieId, Movie = catchingFire };
            ActorRole j = new ActorRole { CharacterName = "Beetee Latier", Actor = jeffrey, ActorId = jeffrey.ActorId, MovieId = catchingFire.MovieId, Movie = catchingFire };
            context.ActorRoles.Add(a);
            context.ActorRoles.Add(b);
            context.ActorRoles.Add(h);
            context.ActorRoles.Add(i);
            context.ActorRoles.Add(j);
            context.ActorRoles.Add(c);
            context.ActorRoles.Add(d);
            context.ActorRoles.Add(e);
            context.ActorRoles.Add(f);
            context.ActorRoles.Add(g);
            context.Movies.Add(catchingFire);
            forum = new DiscussionForum { Movie = catchingFire };
            context.Forums.Add(forum);


            // Avengers Endgame - samuel l
            Actor chrisHemsworth = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/4/4c/Chris_Hemsworth_by_Gage_Skidmore.jpg", CopyrightMessage = "Gage Skidmore, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Chris Hemsworth", Bio = "Christopher Hemsworth was born on August 11, 1983 in Melbourne, Victoria, Australia to Leonie Hemsworth (née van Os), an English teacher & Craig Hemsworth, a social-services counselor. His brothers are actors, Liam Hemsworth & Luke Hemsworth; he is of Dutch (from his immigrant maternal grandfather), Irish, English, Scottish, and German ancestry. His uncle, by marriage, was Rod Ansell, the bushman who inspired the film Crocodile Dundee (1986)." };
            context.Actors.Add(chrisHemsworth);
            Actor robert = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/0/02/Robert_Downey%2C_Jr._2010.jpg", CopyrightMessage = "", FullName = "Robert Downey Jr.", Bio = "Robert Downey Jr. has evolved into one of the most respected actors in Hollywood. With an amazing list of credits to his name, he has managed to stay new and fresh even after over four decades in the business." };
            context.Actors.Add(robert);
            Actor mark = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/c/ce/Mark_Ruffalo_June_2014.jpg", CopyrightMessage = "", FullName = "Mark Ruffalo", Bio = "Award-winning actor Mark Ruffalo was born on November 22, 1967, in Kenosha, Wisconsin, of humble means to father Frank Lawrence Ruffalo, a construction painter and Marie Rose (Hebert), a stylist and hairdresser; his father's ancestry is Italian and his mother is of half French-Canadian and half Italian descent. Mark moved with his family to Virginia Beach, Virginia, where he lived out most of his teenage years. Following high school, Mark moved with his family to San Diego and soon migrated north, eventually settling in Los Angeles." };
            context.Actors.Add(mark);
            Actor tom = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/3/3c/Tom_Holland_by_Gage_Skidmore.jpg", CopyrightMessage = "Gage Skidmore, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Tom Holland", Bio = "Thomas Stanley Holland was born in Kingston-upon-Thames, Surrey, to Nicola Elizabeth (Frost), a photographer, and Dominic Holland (Dominic Anthony Holland), who is a comedian and author. His paternal grandparents were from the Isle of Man and Ireland, respectively. He lives with his parents and three younger brothers - Paddy and twins Sam and Harry. Tom attended Donhead Prep School. Then, after a successful eleven plus exam, he became a pupil at Wimbledon College. Having successfully completed his GCSEs, in September 2012 Tom started a two-year course in the BRIT School for Performing Arts & Technology notable for its numerous famous alumni." };
            context.Actors.Add(tom);
            Actor benedict = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b6/Benedict_Cumberbatch_2016.jpg", CopyrightMessage = "Gage Skidmore, CC BY-SA 2.5 <https://creativecommons.org/licenses/by-sa/2.5>, via Wikimedia Commons", FullName = "Benedict Cumberbatch", Bio = "Benedict Timothy Carlton Cumberbatch was born and raised in London, England. His parents, Wanda Ventham and Timothy Carlton (born Timothy Carlton Congdon Cumberbatch), are both actors. He is a grandson of submarine commander Henry Carlton Cumberbatch, and a great-grandson of diplomat Henry Arnold Cumberbatch CMG. Cumberbatch attended Brambletye School and Harrow School. Whilst at Harrow, he had an arts scholarship and painted large oil canvases. It's also where he began acting. After he finished school, he took a year off to volunteer as an English teacher in a Tibetan monastery in Darjeeling, India. On his return, he studied drama at Manchester University. He continued his training as an actor at the London Academy of Music and Dramatic Art graduating with an M.A. in Classical Acting. By the time he had completed his studies, he already had an agent." };
            context.Actors.Add(benedict);
            Actor scarlett = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/d/db/Scarlett_Johansson_2003.jpg", CopyrightMessage = "Tony Shek, CC BY 2.0 <https://creativecommons.org/licenses/by/2.0>, via Wikimedia Commons", FullName = "Scarlett Johanson", Bio = "Scarlett Ingrid Johansson was born on November 22, 1984 in Manhattan, New York City, New York. Her mother, Melanie Sloan is from a Jewish family from the Bronx and her father, Karsten Johansson is a Danish-born architect from Copenhagen. She has a sister, Vanessa Johansson, who is also an actress, a brother, Adrian, a twin brother, Hunter Johansson, born three minutes after her, and a paternal half-brother, Christian. Her grandfather was writer Ejner Johansson." };
            context.Actors.Add(scarlett);
            Actor paul = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/a/a1/Paul_Rudd_%28cropped%29.jpg", CopyrightMessage = "Red Carpet Report on Mingle Media TV from Culver City, USA, CC BY-SA 2.0 <https://creativecommons.org/licenses/by-sa/2.0>, via Wikimedia Commons", FullName = "Paul Rudd", Bio = "Paul Stephen Rudd was born in Passaic, New Jersey. His parents, Michael and Gloria, both from Jewish families, were born in the London area, U.K. He has one sister, who is three years younger than he is. Paul traveled with his family during his early years, because of his father's airline job at TWA. His family eventually settled in Overland Park, Kansas, where his mother worked as a sales manager for TV station KSMO-TV. Paul attended Broadmoor Junior High and Shawnee Mission West High School, from which he graduated in 1987, and where he was Student Body President. He then enrolled at the University of Kansas in Lawrence, majoring in theater. He graduated from the American Academy of Dramatic Arts-West in Los Angeles and participated in a three-month intensive workshop under the guidance of Michael Kahn at the British Drama Academy at Oxford University in Britain." };
            context.Actors.Add(paul);
            Actor chrisEvans = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/2/25/Chris_Evans_SDCC_2014.jpg", CopyrightMessage = "Gage Skidmore, CC BY-SA 2.0 <https://creativecommons.org/licenses/by-sa/2.0>, via Wikimedia Commons", FullName = "Chris Evans", Bio = "Christopher Robert Evans began his acting career in typical fashion: performing in school productions and community theatre." };
            context.Actors.Add(chrisEvans);
            Actor chrisPratt = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/0/05/Chris_Pratt_%2829427541570%29.jpg", CopyrightMessage = "Gordon  Correll, CC BY-SA 2.0 <https://creativecommons.org/licenses/by-sa/2.0>, via Wikimedia Commons", FullName = "Chris Pratt", Bio = "Christopher Michael Pratt is an American film and television actor. He came to prominence for his small-screen roles, including Bright Abbott in Everwood (2002), Ché in The O.C. (2003), and Andy Dwyer and Parks and Recreation (2009), and notable film roles in Moneyball (2011), The Five-Year Engagement (2012), Zero Dark Thirty (2012), Delivery Man (2013), and Her (2013)." };
            context.Actors.Add(chrisPratt);
            Actor karen = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/1/1a/Karen_Gillan_by_Gage_Skidmore.jpg", CopyrightMessage = "Gage Skidmore, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Karen Gillian", Bio = "Karen Sheila Gillan was born and raised in Inverness, Scotland, as the only child of Marie Paterson and husband John Gillan, who is a singer and recording artist. She developed a love for acting very early on, attending several youth theatre groups and taking part in a wide range of productions at her school, Charleston Academy." };
            context.Actors.Add(karen);
            List<Actor> endgameCast = new List<Actor>
            {
                robert,
                chrisEvans,
                chrisHemsworth,
                samuel,
                mark,
                tom,
                benedict,
                scarlett,
                karen,
                chrisPratt,
                paul
            };
            List<Genre> endgameGenders = new List<Genre>
            {
                superhero,
                action,
                scifi
            };
            Movie endgame = new Movie
            {
                PosterUrl = "https://upload.wikimedia.org/wikipedia/en/0/0d/Avengers_Endgame_poster.jpg",
                CopyrightMessage = "",
                Title = "Avengers: Endgame",
                Director = "Anthony Russo",
                Year = 2019,
                Cast = endgameCast,
                TrailerUrl = "https://www.youtube.com/embed/TcMBFSGVi1c",
                Released = true,
                Genres = endgameGenders,
                Description = "The grave course of events set in motion by Thanos, that wiped out half the universe and fractured the Avengers ranks, compels the remaining Avengers to take one final stand in Marvel Studios' grand conclusion to twenty-two films - Avengers: Endgame. After half of all life is snapped away by Thanos, the Avengers are left scattered and divided."
            };
            a = new ActorRole { CharacterName = "Tony Stark/ Iron Man", Actor = robert, ActorId = robert.ActorId, MovieId = endgame.MovieId, Movie = endgame };
            b = new ActorRole { CharacterName = "Steve Rogers/ Captain America", Actor = chrisEvans, ActorId = chrisEvans.ActorId, MovieId = endgame.MovieId, Movie = endgame };
            c = new ActorRole { CharacterName = "Thor", Actor = chrisHemsworth, ActorId = chrisHemsworth.ActorId, MovieId = endgame.MovieId, Movie = endgame };
            d = new ActorRole { CharacterName = "Natasha Romanoff/ Black Widow", Actor = scarlett, ActorId = scarlett.ActorId, MovieId = endgame.MovieId, Movie = endgame };
            e = new ActorRole { CharacterName = "Bruce Banner/ The Hulk", Actor = mark, ActorId = mark.ActorId, MovieId = endgame.MovieId, Movie = endgame };
            f = new ActorRole { CharacterName = "Scott Lang/ Ant-Man", Actor = paul, ActorId = paul.ActorId, MovieId = endgame.MovieId, Movie = endgame };
            g = new ActorRole { CharacterName = "Nebula", Actor = karen, ActorId = karen.ActorId, MovieId = endgame.MovieId, Movie = endgame };
            h = new ActorRole { CharacterName = "Dr. Stephen Strange", Actor = benedict, ActorId = benedict.ActorId, MovieId = endgame.MovieId, Movie = endgame };
            i = new ActorRole { CharacterName = "Peter Parker/ Spider-Man", Actor = tom, ActorId = tom.ActorId, MovieId = endgame.MovieId, Movie = endgame };
            j = new ActorRole { CharacterName = "Peter Quill/ Star Lord", Actor = chrisPratt, ActorId = chrisPratt.ActorId, MovieId = endgame.MovieId, Movie = endgame };
            ActorRole k = new ActorRole { CharacterName = "Nick Fury", Actor = samuel, ActorId = samuel.ActorId, MovieId = endgame.MovieId, Movie = endgame };
            context.ActorRoles.Add(a);
            context.ActorRoles.Add(b);
            context.ActorRoles.Add(c);
            context.ActorRoles.Add(d);
            context.ActorRoles.Add(e);
            context.ActorRoles.Add(f);
            context.ActorRoles.Add(g);
            context.ActorRoles.Add(h);
            context.ActorRoles.Add(i);
            context.ActorRoles.Add(j);
            context.ActorRoles.Add(k);
            context.Movies.Add(endgame);
            forum = new DiscussionForum { Movie = endgame };
            context.Forums.Add(forum);


            // Kill Bill - madsen
            Actor uma = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/b/be/Uma_Thurman_2014_%28cropped%29.jpg", CopyrightMessage = "Siebbi, CC BY 3.0 <https://creativecommons.org/licenses/by/3.0>, via Wikimedia Commons", FullName = "Uma Thurman", Bio = "Uma Karuna Thurman was born in Boston, Massachusetts, into a highly unorthodox and internationally-minded family. She is the daughter of Nena Thurman (née Birgitte Caroline von Schlebrügge), a fashion model and socialite who now runs a mountain retreat, and of Robert Thurman (Robert Alexander Farrar Thurman), a professor and academic who is one of the nation's foremost Buddhist scholars. Uma's mother was born in Mexico City, Mexico, to a German father and a Swedish mother (who herself was of Swedish, Danish, and German descent). Uma's father, a New Yorker, has English, Scots-Irish, Scottish, and German ancestry. Uma grew up in Amherst, Massachusetts, where her father worked at Amherst College." };
            context.Actors.Add(uma);
            Actor david = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/7/7f/David_Carradine_Polanski_Unauthorized.jpg", CopyrightMessage = "Credit to lukeford.net (permission statement at en:User:Tabercil/Luke Ford permission), CC BY-SA 2.5 <https://creativecommons.org/licenses/by-sa/2.5>, via Wikimedia Commons", FullName = "David Carradine", Bio = "David Carradine was born in Hollywood, California, the eldest son of legendary character actor John Carradine, and his wife, Ardanelle Abigail (McCool). He was a member of an acting family that included brothers Keith Carradine and Robert Carradine as well as his daughters Calista Carradine and Kansas Carradine, and nieces Ever Carradine and Martha Plimpton." };
            context.Actors.Add(david);
            Actor vivica = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/b/ba/Vivica_A._Fox_%2833845756800%29_%28cropped%29.jpg", CopyrightMessage = "Gage Skidmore from Peoria, AZ, United States of America, CC BY-SA 2.0 <https://creativecommons.org/licenses/by-sa/2.0>, via Wikimedia Commons", FullName = "Vivica A. Fox", Bio = "Vivica A. Fox was born in South Bend, Indiana, on July 30, 1964, and is the daughter of Everlyena, a pharmaceutical technician, and William Fox, a private school administrator. She is of Native American and African-American descent and is proud of her heritage. She is a graduate of Arlington High School in Indianapolis, Indiana, and, after graduating, moved to California to attend college. Vivica went to Golden West College and graduated with an Associate Art degree in Social Sciences. While in California, she started acting professionally, first on soap operas, such as Generations (1989), Days of Our Lives (1965) and The Young and the Restless (1973)." };
            context.Actors.Add(vivica);
            Actor lucy = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b0/Lucy_Liu_Cannes_2008.jpg", CopyrightMessage = "Georges Biard, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Lucy Liu", Bio = "Born to immigrants in Queens, New York, Lucy Liu has always tried to balance an interest in her cultural heritage with a desire to move beyond a strictly Asian-American experience. Lucy's mother, Cecilia, a biochemist, is from Beijing, and her father, Tom Liu, a civil engineer, is from Shanghai. Once relegated to 'ethnic' parts, the energetic actress is finally earning her stripes as an across-the-board leading lady." };
            context.Actors.Add(lucy);
            Actor daryl = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/5/55/Daryl_Hannah_2013.jpg", CopyrightMessage = "World Travel & Tourism Council, CC BY 2.0 <https://creativecommons.org/licenses/by/2.0>, via Wikimedia Commons", FullName = "Daryl Hannah", Bio = "Daryl Christine Hannah was born and raised in Chicago, Illinois. She is the daughter of Susan Jeanne (Metzger), a schoolteacher and later a producer, and Donald Christian Hannah, who owned a tugboat/barge company. Her stepfather was music journalist/promoter Jerrold Wexler. Her siblings are Page Hannah, Don Hannah and Tanya Wexler. She has Scottish, Norwegian, Danish, Irish, English, and German ancestry." };
            context.Actors.Add(daryl);
            Actor julie = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/3/36/Julie_Dreyfus_Gerardmer_2007.jpg", CopyrightMessage = "", FullName = "Julie Dreyfus", Bio = "Julie Dreyfus was born in Paris, France. After studying interior design, she moved to Japan as a young adult and made her debut on Japanese T.V. She became fluent in Japanese and English and appeared in several American movies as well. She is very well known in Japan but is also known internationally for her roles in Kill Bill: Vol. 1 (2003), Kill Bill: Vol. 2 (2004) and Inglourious Basterds (2009)." };
            context.Actors.Add(julie);
            List<Actor> killbillCast = new List<Actor>
            {
                uma,
                david,
                daryl,
                michaelMadsen,
                lucy,
                vivica,
                julie,
                samuel
            };
            List<Genre> killBillGenres = new List<Genre>
            {
                action,
                thriller
            };
            Movie killBill = new Movie
            {
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BNzM3NDFhYTAtYmU5Mi00NGRmLTljYjgtMDkyODQ4MjNkMGY2XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_QL75_UX190_CR0,2,190,281_.jpg",
                CopyrightMessage = "",
                Title = "Kill Bill: Vol 1",
                Director = "Quentin Tarantino",
                Year = 2003,
                TrailerUrl = "https://www.youtube.com/embed/7kSuas6mRpk",
                Genres = killBillGenres,
                Cast = killbillCast,
                Released = true,
                Description = "Kill Bill is the story of one retired assassin's revenge against a man who tried to kill her while she was pregnant years prior. After being in a coma for four years, Beatrix Kiddo is hungry for revenge against the man and his team of assassins and will stop at nothing to Kill Bill."
            };
            a = new ActorRole { CharacterName = "Beatrix Kiddo/ The Bride/ Black Mamba", Actor = uma, ActorId = uma.ActorId, MovieId = killBill.MovieId, Movie = killBill };
            b = new ActorRole { CharacterName = "Bill/ Snake Charmer", Actor = david, ActorId = david.ActorId, MovieId = killBill.MovieId, Movie = killBill };
            c = new ActorRole { CharacterName = "Elle Driver/ California Mountain Snake", Actor = daryl, ActorId = daryl.ActorId, MovieId = killBill.MovieId, Movie = killBill };
            d = new ActorRole { CharacterName = "Budd/ Sidewinder", Actor = michaelMadsen, ActorId = michaelMadsen.ActorId, MovieId = killBill.MovieId, Movie = killBill };
            e = new ActorRole { CharacterName = "O-Ren Ishii/ Cottonmouth", Actor = lucy, ActorId = lucy.ActorId, MovieId = killBill.MovieId, Movie = killBill };
            f = new ActorRole { CharacterName = "Vernita Green/ Copperhead", Actor = vivica, ActorId = vivica.ActorId, MovieId = killBill.MovieId, Movie = killBill };
            g = new ActorRole { CharacterName = "Sofie Fatale", Actor = julie, ActorId = julie.ActorId, MovieId = killBill.MovieId, Movie = killBill };
            h = new ActorRole { CharacterName = "Rufus", Actor = samuel, ActorId = samuel.ActorId, MovieId = killBill.MovieId, Movie = killBill };
            context.ActorRoles.Add(a);
            context.ActorRoles.Add(b);
            context.ActorRoles.Add(c);
            context.ActorRoles.Add(d);
            context.ActorRoles.Add(e);
            context.ActorRoles.Add(f);
            context.ActorRoles.Add(g);
            context.ActorRoles.Add(h);
            context.Movies.Add(killBill);
            forum = new DiscussionForum { Movie = killBill };
            context.Forums.Add(forum);


            // The Good, the bad and the ugly
            Actor clint = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/7/7e/Clint_Eastwood_at_2010_New_York_Film_Festival.jpg", CopyrightMessage = "Raffi Asdourian, CC BY 2.0 <https://creativecommons.org/licenses/by/2.0>, via Wikimedia Commons", FullName = "Clint Eastwood", Bio = "Clint Eastwood was born May 31, 1930 in San Francisco, the son of Clinton Eastwood Sr., a bond salesman and later manufacturing executive for Georgia-Pacific Corporation, and Ruth Wood, a housewife turned IBM operator. He had a comfortable, middle-class upbringing in nearby Piedmont. At school Clint took interest in music and mechanics, but was an otherwise bored student; this resulted in being held back a grade. When Eastwood was 19, his parents relocated to Washington state, and young Clint spent a couple years working menial jobs in the Pacific Northwest. Returning to California in 1951, he did a stint at Fort Ord Military Reservation and later enrolled at Los Angeles City College, but dropped out after two semesters to pursue acting." };
            context.Actors.Add(clint);
            Actor eli = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/a/a8/Eli_Wallach_-_publicity.jpg", CopyrightMessage = "", FullName = "Eli Wallach", Bio = "One of Hollywood's finest character / method actors, Eli Wallach was in demand for over 60 years (first film/TV role was 1949) on stage and screen, and has worked alongside the world's biggest stars, including Clark Gable, Clint Eastwood, Steve McQueen, Marilyn Monroe, Yul Brynner, Peter O'Toole, and Al Pacino, to name but a few." };
            context.Actors.Add(eli);
            Actor lee = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/8/8d/Lee_Van_Cleef_in_Kansas_City_Confidential.jpg", CopyrightMessage = "", FullName = "Lee Van Cleef", Bio = "One of the great movie villains, Clarence Leroy Van Cleef, Jr. was born in Somerville, New Jersey, to Marion Lavinia (Van Fleet) and Clarence LeRoy Van Cleef, Sr. His parents were both of Dutch ancestry. Van Cleef started out as an accountant. He served in the U.S. Navy aboard minesweepers and sub chasers during World War II. After the war he worked as an office administrator, becoming involved in amateur theatrics in his spare time. An audition for a professional role led to a touring company job in 'Mr.Roberts'. His performance was seen by Stanley Kramer, who cast him as henchman Jack Colby in High Noon (1952), a role that brought him great recognition despite the fact that he had no dialogue." };
            context.Actors.Add(lee);
            List<Actor> gbuCast = new List<Actor>
            {
                clint,
                eli,
                lee
            };
            List<Genre> gbuGenres = new List<Genre>
            {
                western,
                action,
                comedy
            };
            Movie gbu = new Movie
            {
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BOTQ5NDI3MTI4MF5BMl5BanBnXkFtZTgwNDQ4ODE5MDE@._V1_.jpg",
                CopyrightMessage = "",
                Title = "The Good, The Bad and The Ugly",
                Director = "Sergio Leone",
                Year = 1966,
                Cast = gbuCast,
                TrailerUrl = "https://www.youtube.com/embed/WCN5JJY_wiA",
                Genres = gbuGenres,
                Released = true,
                Description = "Blondie (The Good) and Tuco (The Ugly) are con artists, trying to steal as much money as possible during the Civil War. Angel Eyes (The Bad) is trying to locate a bank robber going by the name of Bill Carson, in order to collect his stolen gold. After a few scams, Blondie cheats Tuco by keeping his share and leaving him in the dust."
            };
            a = new ActorRole { CharacterName = "Blondie", Actor = clint, ActorId = clint.ActorId, MovieId = gbu.MovieId, Movie = gbu };
            b = new ActorRole { CharacterName = "Tuco", Actor = eli, ActorId = eli.ActorId, MovieId = gbu.MovieId, Movie = gbu };
            c = new ActorRole { CharacterName = "Angel Eyes", Actor = lee, ActorId = lee.ActorId, MovieId = gbu.MovieId, Movie = gbu };


            context.ActorRoles.Add(a);
            context.ActorRoles.Add(b);
            context.ActorRoles.Add(c);
            context.Movies.Add(gbu);
            forum = new DiscussionForum { Movie = gbu };
            context.Forums.Add(forum);


            // Kingsmen - samuell 
            Actor colin = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/f/f8/Colin_firth.jpg", CopyrightMessage = "Aminatk, CC BY-SA 4.0 <https://creativecommons.org/licenses/by-sa/4.0>, via Wikimedia Commons", FullName = "Colin Firth", Bio = "Colin Andrew Firth was born into an academic family in Grayshott, Hampshire, England. His mother, Shirley Jean (Rolles), was a comparative religion lecturer at the Open University, and his father, David Norman Lewis Firth, lectured on history at Winchester University College (formerly King Alfred's College) in Winchester, and worked on education for the Nigerian government. His grandparents were missionaries. His siblings Katie Firth and Jonathan Firth are also actors." };
            context.Actors.Add(colin);
            Actor michaelCaine = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/f/f4/Sir_Michael_Caine%2C_28th_EFA_Awards_2015%2C_Berlin_%28cropped%29.jpg", CopyrightMessage = "TonkBerlin, CC BY-SA 4.0 <https://creativecommons.org/licenses/by-sa/4.0>, via Wikimedia Commons", FullName = "Michael Caine", Bio = "Michael Caine was born Maurice Joseph Micklewhite in London, to Ellen Frances Marie (Burchell), a charlady, and Maurice Joseph Micklewhite, a fish-market porter. He left school at age 15 and took a series of working-class jobs before joining the British army and serving in Korea during the Korean War, where he saw combat. Upon his return to England, he gravitated toward the theater and got a job as an assistant stage manager. He adopted the name of Caine on the advice of his agent, taking it from a marquee that advertised The Caine Mutiny (1954)." };
            context.Actors.Add(michaelCaine);
            Actor taron = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/9/9a/Taron-egerton-gesf-2018-5667.jpg", CopyrightMessage = "Fuzheado, CC BY-SA 4.0 <https://creativecommons.org/licenses/by-sa/4.0>, via Wikimedia Commons", FullName = "Taron Egerton", Bio = "Taron Egerton is a British actor and singer, known for his roles in the British television series The Smoke, the 2014 action comedy film Kingsman: The Secret Service, and the film Rocketman (2019). He has also played Edward Brittain in the 2014 drama film Testament of Youth, appeared in the 2015 crime thriller film Legend, starred as Eddie 'The Eagle' Edwards in the 2016 biographical film Eddie the Eagle, voiced Johnny in the 2016 animated musical film Sing, and reprised his role in the 2017 Kingsman sequel, The Golden Circle." };
            context.Actors.Add(taron);
            Actor sofia = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/e/ed/Sofia_Boutella_2018.jpg", CopyrightMessage = "https://vimeo.com/ColliderVideo, CC BY 3.0 <https://creativecommons.org/licenses/by/3.0>, via Wikimedia Commons", FullName = "Sofia Boutella", Bio = "Sofia Boutella is an Algerian actress, dancer and model. She was born in the Bab El Oued district of Algiers in Algeria, the daughter of composer and jazz musician Safy Boutella, and an architect mother. She started classical dance education when she was five years old. In 1992, at age 10, she left Algeria with her family and moved to France, where she started rhythmic gymnastics, joining the French national team at age 18. Sofia started hip hop and street dance, and was part of a group called the Vagabond Crew. She also participated in a group called Chienne de Vie and Aphrodites. She has been rehearsing since age 17 with choreographer Blanca Li, and danced in several film and television appearances, as well as commercials and concert tours." };
            context.Actors.Add(sofia);
            Actor jackDavenport = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/a/a5/Jack_Davenport_in_White_Famous_S01_E03.jpg", CopyrightMessage = "White Famous; The official YouTube channel for the SHOWTIME series White Famous., CC BY 3.0 <https://creativecommons.org/licenses/by/3.0>, via Wikimedia Commons", FullName = "Jack Davenport", Bio = "Jack Davenport was born in 1973 and is the son of actors Maria Aitken and Nigel Davenport. He studied Literature and Film Studies at the University of East Anglia. His first break happened after he wrote to John Cleese to ask to be a runner on Fierce Creatures (1997) where he ended up playing a zoo keeper. His first major role however was that of public school educated barrister Miles in the BBC television series This Life (1996). Recent projects include the stylish Ultraviolet (1998) where he played a modern-day vampire hunter, The Talented Mr. Ripley (1999) as Matt Damon's love interest, and Pirates of the Caribbean: The Curse of the Black Pearl (2003) as the Keira Knightley's intended mate" };
            context.Actors.Add(jackDavenport);
            List<Actor> kingsmenCast = new List<Actor>
            {
                colin,
                taron,
                samuel,
                michaelCaine,
                jackDavenport,
                sofia
            };
            List<Genre> kingsmenGenres = new List<Genre>
            {
                spy,
                action
            };
            Movie kingsmen = new Movie
            {
                PosterUrl = "https://upload.wikimedia.org/wikipedia/en/8/8b/Kingsman_The_Secret_Service_poster.jpg",
                CopyrightMessage = "",
                Title = "Kingsman: The Secret Service",
                Director = "Matthew Vaughn",
                Year = 2015,
                Cast = kingsmenCast,
                Genres = kingsmenGenres,
                TrailerUrl = "https://www.youtube.com/embed/kl8F-8tR8to",
                Released = true,
                Description = "The film follows the recruitment and training of Gary 'Eggsy' Unwin (Taron Egerton), into a secret spy organisation. In a both brutal and comedic fashion, Eggsy joins a mission to tackle a global threat from Richmond Valentine (Samuel L. Jackson), a wealthy megalomaniac wanting to deal with climate change."
            };
            a = new ActorRole { CharacterName = "Gary 'Eggsy' Unwin", Actor = taron, ActorId = taron.ActorId, MovieId = kingsmen.MovieId, Movie = kingsmen };
            b = new ActorRole { CharacterName = "Harry Hart/ Galahad", Actor = colin, ActorId = colin.ActorId, MovieId = kingsmen.MovieId, Movie = kingsmen };
            c = new ActorRole { CharacterName = "Richmond Valentine", Actor = samuel, ActorId = samuel.ActorId, MovieId = kingsmen.MovieId, Movie = kingsmen };
            d = new ActorRole { CharacterName = "Arthur", Actor = michaelCaine, ActorId = michaelCaine.ActorId, MovieId = kingsmen.MovieId, Movie = kingsmen };
            e = new ActorRole { CharacterName = "Gazelle", Actor = sofia, ActorId = sofia.ActorId, MovieId = kingsmen.MovieId, Movie = kingsmen };
            f = new ActorRole { CharacterName = "Lancelot", Actor = jackDavenport, ActorId = jackDavenport.ActorId, MovieId = kingsmen.MovieId, Movie = kingsmen };

            context.ActorRoles.Add(a);
            context.ActorRoles.Add(b);
            context.ActorRoles.Add(c);
            context.ActorRoles.Add(d);
            context.ActorRoles.Add(e);
            context.ActorRoles.Add(f);
            context.Movies.Add(kingsmen);
            forum = new DiscussionForum { Movie = kingsmen };
            context.Forums.Add(forum);


            // Pirates of the Caribbean - jack d
            Actor johnny = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/3/3b/Johnny_Depp-2757_%28cropped%29.jpg", CopyrightMessage = "Harald Krichel, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Johnny Depp", Bio = "Johnny Depp is perhaps one of the most versatile actors of his day and age in Hollywood. He was born John Christopher Depp II in Owensboro, Kentucky, on June 9, 1963, to Betty Sue (Wells), who worked as a waitress, and John Christopher Depp, a civil engineer. Depp was raised in Florida. He dropped out of school when he was 15, and fronted a series of music-garage bands, including one named 'The Kids'. When he married Lori Anne Allison (Lori A. Depp) he took a job as a ballpoint-pen salesman to support himself and his wife. A visit to Los Angeles, California, with his wife, however, happened to be a blessing in disguise, when he met up with actor Nicolas Cage, who advised him to turn to acting, which culminated in Depp's film debut in the low-budget horror film, A Nightmare on Elm Street (1984), where he played a teenager who falls prey to dream-stalking demon Freddy Krueger." };
            context.Actors.Add(johnny);
            Actor keira = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b1/KeiraKnightleyByAndreaRaffin2011_%28cropped%29.jpg", CopyrightMessage = "Andrea Raffin, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Keira Knightley", Bio = "Keira Christina Knightley was born March 26, 1985 in the South West Greater London suburb of Richmond. She is the daughter of actor Will Knightley and actress turned playwright Sharman Macdonald. An older brother, Caleb Knightley, was born in 1979. Her father is English, while her Scottish-born mother is of Scottish and Welsh origin. Brought up immersed in the acting profession from both sides - writing and performing - it is little wonder that the young Keira asked for her own agent at the age of three." };
            context.Actors.Add(keira);
            Actor orlando = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/0/0c/Orlando_Bloom_at_Venice_Festival.jpg", CopyrightMessage = "Hengist Decius, CC BY 2.0 <https://creativecommons.org/licenses/by/2.0>, via Wikimedia Commons", FullName = "Orlando Bloom", Bio = "Orlando Jonathan Blanchard Copeland Bloom was born on January 13, 1977 in Canterbury, Kent, England.His mother, Sonia Constance Josephine Bloom(née Copeland), was born in Kolkata, India, to an English family then - resident there.The man he first knew as his father, Harry Bloom, was a legendary political activist who fought for civil rights in South Africa.But Harry died of a stroke when Orlando was only four years old.After that, Orlando and his older sister, Samantha Bloom, were raised by their mother and family friend, Colin Stone.When Orlando was 13, Sonia revealed to him that Colin is actually the biological father of Orlando and his sister; the two were conceived after an agreement by his parents, since Harry, who suffered a stroke in 1975, was unable to have children." };
            context.Actors.Add(orlando);
            Actor geoffrey = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/4/40/Geoffrey_Rush_%286397243785%29.jpg", CopyrightMessage = "Gordon  Correll, CC BY-SA 2.0 <https://creativecommons.org/licenses/by-sa/2.0>, via Wikimedia Commons", FullName = "Geoffrey Rush", Bio = "Geoffrey Roy Rush was born on July 6, 1951, in Toowoomba, Queensland, Australia, to Merle (Bischof), a department store sales assistant, and Roy Baden Rush, an accountant for the Royal Australian Air Force. His mother was of German descent and his father had English, Irish, and Scottish ancestry. He was raised in Brisbane, Queensland, after his parents split up." };
            context.Actors.Add(geoffrey);
            Actor jonathan = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/1/10/JonathanPryce2007_cropped.jpg", CopyrightMessage = "JonathanPryce2007.jpg: Honeyfitzderivative work: Jan Arkesteijn, CC BY-SA 2.0 <https://creativecommons.org/licenses/by-sa/2.0>, via Wikimedia Commons", FullName = "Jonathan Pryce", Bio = "From Holywell to Hollywood, Jonathan Pryce, CBE is a critically acclaimed and award winning actor from North Wales, internationally regarded as one of the greatest ones on both stage and screen. Renowned for his immense versatility and powerful presence he has transcended the industry, effortlessly moving between Hollywood studio films and the classical West End in a range of work that also includes bold independent cinema and the Broadway musical. He has created some of the most memorable characters in cinema and for over forty years been on the wish list of many of the worlds most notorious and accomplished directors as 'Someone they must work with'." };
            context.Actors.Add(jonathan);
            List<Actor> potcCast = new List<Actor>
            {
                johnny,
                orlando,
                keira,
                geoffrey,
                jackDavenport,
                jonathan
            };
            List<Genre> potcGenres = new List<Genre>
            {
                adventure,
                family,
                comedy
            };
            Movie potc = new Movie
            {
                PosterUrl = "https://upload.wikimedia.org/wikipedia/en/8/89/Pirates_of_the_Caribbean_-_The_Curse_of_the_Black_Pearl.png",
                CopyrightMessage = "",
                Title = "Pirates of the Caribbean: The Curse of the Black Pearl",
                Director = "Gore Verbinski",
                Year = 2003,
                TrailerUrl = "https://www.youtube.com/embed/naQr0uTrH_s",
                Cast = potcCast,
                Released = true,
                Genres = potcGenres,
                Description = "The story follows pirate Jack Sparrow (Johnny Depp) and blacksmith Will Turner (Orlando Bloom) as they rescue the kidnapped Elizabeth Swann (Keira Knightley) from the cursed crew of the Black Pearl, captained by Hector Barbossa (Geoffrey Rush), who become undead skeletons at night."
            };
            a = new ActorRole { CharacterName = "Jack Sparrow", Actor = johnny, ActorId = johnny.ActorId, MovieId = potc.MovieId, Movie = potc };
            b = new ActorRole { CharacterName = "Will Turner", Actor = orlando, ActorId = orlando.ActorId, MovieId = potc.MovieId, Movie = potc };
            c = new ActorRole { CharacterName = "Elizabeth Swann", Actor = keira, ActorId = keira.ActorId, MovieId = potc.MovieId, Movie = potc };
            d = new ActorRole { CharacterName = "Hector Barbossa", Actor = geoffrey, ActorId = geoffrey.ActorId, MovieId = potc.MovieId, Movie = potc };
            e = new ActorRole { CharacterName = "Norrington", Actor = jackDavenport, ActorId = jackDavenport.ActorId, MovieId = potc.MovieId, Movie = potc };
            f = new ActorRole { CharacterName = "Governor Weatherby Swann", Actor = jonathan, ActorId = jonathan.ActorId, MovieId = potc.MovieId, Movie = potc };

            context.ActorRoles.Add(a);
            context.ActorRoles.Add(b);
            context.ActorRoles.Add(c);
            context.ActorRoles.Add(d);
            context.ActorRoles.Add(e);
            context.ActorRoles.Add(f);
            context.Movies.Add(potc);
            forum = new DiscussionForum { Movie = potc };
            context.Forums.Add(forum);


            // the lord of the rings - orlando, cate
            Actor ian = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/e/e4/Ian_McKellen_by_Gage_Skidmore.jpg", CopyrightMessage = "Gage Skidmore, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Ian McKellen", Bio = "Widely regarded as one of greatest stage and screen actors both in his native Great Britain and internationally, twice nominated for the Oscar and recipient of every major theatrical award in the UK and US, Ian Murray McKellen was born on May 25, 1939 in Burnley, Lancashire, England, to Margery Lois (Sutcliffe) and Denis Murray McKellen, a civil engineer and lay preacher. He is of Scottish, Northern Irish, and English descent. During his early childhood, his parents moved with Ian and his older sister, Jean, to the mill town of Wigan. It was in this small town that young Ian rode out World War II. He soon developed a fascination with acting and the theatre, which was encouraged by his parents. They would take him to plays, those by William Shakespeare, in particular. The amateur school productions fostered Ian's growing passion for theatre." };
            context.Actors.Add(ian);
            Actor elijah = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/f/f8/Elijah_Wood-D.jpg", CopyrightMessage = "Dysepsion, CC BY-SA 4.0 <https://creativecommons.org/licenses/by-sa/4.0>, via Wikimedia Commons", FullName = "Elijah Wood", Bio = "Elijah Wood is an American actor best known for portraying Frodo Baggins in Peter Jackson's blockbuster Lord of the Rings film trilogy. In addition to reprising the role in The Hobbit series, Wood also played Ryan in the FX television comedy Wilfred (2011) and voiced Beck in the Disney XD animated television series TRON: Uprising (2012)." };
            context.Actors.Add(elijah);
            Actor seanA = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/8/89/Sean_Astin_by_Gage_Skidmore.jpg", CopyrightMessage = "Gage Skidmore, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Sean Astin", Bio = "Sean Patrick Astin (né Duke; February 25, 1971) is an American actor, voice actor, screenwriter, director, producer, family man, author, marathon runner, political activist and philanthropist who is well known for his film debut portraying Mikey in Steven Spielberg's The Goonies (1985), for playing the title role in the critically acclaimed Rudy (1993), and for his role as the beloved Sam Gamgee in the Academy Award winning trilogy, The Lord of the Rings: The Fellowship of the Ring (2001), The Lord of the Rings: The Two Towers (2002), and The Lord of the Rings: The Return of the King (2003)." };
            context.Actors.Add(seanA);
            Actor billy = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/6/62/MCCC_15_-_Billy_Boyd_02_%2818092751665%29.jpg", CopyrightMessage = "GabboT, CC BY-SA 2.0 <https://creativecommons.org/licenses/by-sa/2.0>, via Wikimedia Commons", FullName = "Billy Boyd", Bio = "Billy Boyd was born in 1968 in Glasgow, Scotland, to Mary and William Boyd. The talented young boy, inspired by Star Wars to try acting, got his first taste of it in his school's production of Oliver Twist when he was 10. Boyd's parents were extremely supportive, driving over two hours to get him to the performances, but sadly they passed away when he was 12. He was thereafter raised by his grandmother. He realized that he enjoyed acting very much and told his school counselor that was what he wanted to be, but the counselor discouraged this choice and told him to keep it secret. When he was 17 he left school and went to work in a book-binding workshop. He worked there 4 years as an apprentice and 2 years as a workman. Ironically, during the years he worked at the book-binders, the Lord of the Rings trilogy was printed and bound there, many copies bound by his hands." };
            context.Actors.Add(billy);
            Actor dominic = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/5/52/Dominic_Monaghan_2003.jpg", CopyrightMessage = "Stefan Servos, CC BY-SA 3.0 <http://creativecommons.org/licenses/by-sa/3.0/>, via Wikimedia Commons", FullName = "Dominic Monaghan", Bio = "Dominic Monaghan is best known for his role in the movie adaptations of Lord of the Rings. Before that, he became known in England for his role in the British television drama Hetty Wainthropp Investigates (1996)." };
            context.Actors.Add(dominic);
            Actor johnDavies = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/8/85/John_Rhys-Davies_byVetulani.JPG", CopyrightMessage = "Franciszek Vetulani, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "John Rhys-Davies", Bio = "Welsh actor John Rhys-Davies was born in Ammanford, Carmarthenshire, Wales, to Mary Margaretta Phyllis (nee Jones), a nurse, and Rhys Davies, a mechanical engineer and Colonial Officer. He graduated from the University of East Anglia and is probably best known to film audiences for his roles in the blockbuster hits Raiders of the Lost Ark (1981) and Indiana Jones and the Last Crusade (1989). He was introduced to a new generation of fans in the blockbuster trilogy The Lord of the Rings (The Lord of the Rings: The Fellowship of the Ring (2001), (The Lord of the Rings: The Two Towers (2002), and (The Lord of the Rings: The Return of the King (2003)) in the role of Gimli the dwarf. He has also had leading roles in Victor Victoria (1982), The Living Daylights (1987) and King Solomon's Mines (1985)." };
            context.Actors.Add(johnDavies);
            Actor seanB = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/a/aa/Sean_Bean_TIFF_2015.jpg", CopyrightMessage = "", FullName = "Sean Bean", Bio = "Sean Bean's career since the eighties spans theatre, radio, television and movies. Bean was born in Handsworth, Sheffield, South Yorkshire, to Rita (Tuckwood) and Brian Bean. He worked for his father's welding firm before he decided to become an actor. He attended RADA in London and appeared in a number of West End stage productions including RSC's Fair Maid of the West (Spencer), (1986) and Romeo and Juliet (1987) (Romeo) , as well as Deathwatch (Lederer) (1985) at the Young Vic and Killing the Cat (Danny) (1990) at the Theatre Upstairs." };
            context.Actors.Add(seanB);
            Actor viggo = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/e/eb/Viggo_Mortensen_C_%282020%29.jpg", CopyrightMessage = "", FullName = "Viggo Mortensen", Bio = "Mortensen was born in New York City, to Grace Gamble (Atkinson) and Viggo Peter Mortensen, Sr. His father was Danish, his mother was American, and his maternal grandfather was Canadian. His parents met in Norway. They wed and moved to New York, where Viggo, Jr. was born, before moving to South America, where Viggo, Sr. managed chicken farms and ranches in Venezuela and Argentina. Two more sons were born, Charles and Walter, before the marriage grew increasingly unhappy. When Viggo was seven, his parents sent him to a a strict boarding school, isolated in the foothills of the mountains of Argentina. Then, at age eleven, his parents divorced. His mother moved herself and the children back to her home state of New York." };
            context.Actors.Add(viggo);
            Actor christopher = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/0/0d/Christopher_Lee%2C_Women%27s_World_Awards_2009_a.jpg", CopyrightMessage = "Manfred Werner - Tsui, CC BY-SA 3.0 <http://creativecommons.org/licenses/by-sa/3.0/>, via Wikimedia Commons", FullName = "Christopher Lee", Bio = "Sir Christopher Frank Carandini Lee was perhaps the only actor of his generation to have starred in so many films and cult saga. Although most notable for personifying bloodsucking vampire, Dracula, on screen, he portrayed other varied characters on screen, most of which were villains, whether it be Francisco Scaramanga in the James Bond film, The Man with the Golden Gun (1974), or Count Dooku in Star Wars: Episode II - Attack of the Clones (2002), or as the title monster in the Hammer Horror film, The Mummy (1959)." };
            context.Actors.Add(christopher);
            Actor andy = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/7/79/Andy_Serkis_2017.jpg", CopyrightMessage = "Gage Skidmore from Peoria, AZ, United States of America, CC BY-SA 2.5 <https://creativecommons.org/licenses/by-sa/2.5>, via Wikimedia Commons", FullName = "Andy Serkis", Bio = "English film actor, director and author Andy Serkis is known for his performance capture roles comprising motion capture acting, animation and voice work for such computer-generated characters as Gollum in The Lord of the Rings film trilogy (2001-2003) and The Hobbit: An Unexpected Journey (2012), the eponymous King Kong in the 2005 film, Caesar in Rise of the Planet of the Apes (2011) and Dawn of the Planet of the Apes (2014), Captain Haddock / Sir Francis Haddock in Steven Spielberg's The Adventures of Tintin: The Secret of the Unicorn (2011) and Supreme Leader Snoke in Star Wars: Episode VII - The Force Awakens (2015)." };
            context.Actors.Add(andy);
            List<Actor> lotrCast = new List<Actor>
            {
                elijah,
                ian,
                seanA,
                viggo,
                orlando,
                seanB,
                christopher,
                andy,
                billy,
                dominic,
                cate,
                johnDavies
            };
            List<Genre> lotrGenres = new List<Genre>
            {
                family,
                fantasy,
                adventure
            };
            Movie lotr = new Movie
            {
                PosterUrl = "https://upload.wikimedia.org/wikipedia/en/8/8a/The_Lord_of_the_Rings_The_Fellowship_of_the_Ring_%282001%29.jpg",
                CopyrightMessage = "",
                Title = "The Lord of The Rings: The Fellowship of the Ring",
                Director = "Peter Jackson",
                Year = 2001,
                Genres = lotrGenres,
                TrailerUrl = "https://www.youtube.com/embed/V75dMMIW2B4",
                Released = true,
                Cast = lotrCast,
                Description = "An ancient Ring thought lost for centuries has been found, and through a strange twist of fate has been given to a small Hobbit named Frodo. When Gandalf discovers the Ring is in fact the One Ring of the Dark Lord Sauron, Frodo must make an epic quest to the Cracks of Doom in order to destroy it. However, he does not go alone. He is joined by Gandalf, Legolas the elf, Gimli the Dwarf, Aragorn, Boromir, and his three Hobbit friends Merry, Pippin, and Samwise. Through mountains, snow, darkness, forests, rivers and plains, facing evil and danger at every corner the Fellowship of the Ring must go. Their quest to destroy the One Ring is the only hope for the end of the Dark Lords reign."
            };
            a = new ActorRole { CharacterName = "Frodo Baggins", Actor = elijah, ActorId = elijah.ActorId, MovieId = lotr.MovieId, Movie = lotr };
            b = new ActorRole { CharacterName = "Samwise Gamgee", Actor = seanA, ActorId = seanA.ActorId, MovieId = lotr.MovieId, Movie = lotr };
            c = new ActorRole { CharacterName = "Gandalf the Grey", Actor = ian, ActorId = ian.ActorId, MovieId = lotr.MovieId, Movie = lotr };
            d = new ActorRole { CharacterName = "Aragorn", Actor = viggo, ActorId = viggo.ActorId, MovieId = lotr.MovieId, Movie = lotr };
            e = new ActorRole { CharacterName = "Legolas", Actor = orlando, ActorId = orlando.ActorId, MovieId = lotr.MovieId, Movie = lotr };
            f = new ActorRole { CharacterName = "Gimli", Actor = johnDavies, ActorId = johnDavies.ActorId, MovieId = lotr.MovieId, Movie = lotr };
            g = new ActorRole { CharacterName = "Galadriel", Actor = cate, ActorId = cate.ActorId, MovieId = lotr.MovieId, Movie = lotr };
            h = new ActorRole { CharacterName = "Meriadoc 'Merry' Brandybuck", Actor = dominic, ActorId = dominic.ActorId, MovieId = lotr.MovieId, Movie = lotr };
            i = new ActorRole { CharacterName = "Peregrin 'Pippin' Took", Actor = billy, ActorId = billy.ActorId, MovieId = lotr.MovieId, Movie = lotr };
            j = new ActorRole { CharacterName = "Boromir", Actor = seanB, ActorId = seanB.ActorId, MovieId = lotr.MovieId, Movie = lotr };
            k = new ActorRole { CharacterName = "Saruman the White", Actor = christopher, ActorId = christopher.ActorId, MovieId = lotr.MovieId, Movie = lotr };
            ActorRole l = new ActorRole { CharacterName = "Gollum", Actor = andy, ActorId = andy.ActorId, MovieId = lotr.MovieId, Movie = lotr };
            context.ActorRoles.Add(a);
            context.ActorRoles.Add(b);
            context.ActorRoles.Add(c);
            context.ActorRoles.Add(d);
            context.ActorRoles.Add(e);
            context.ActorRoles.Add(f);
            context.ActorRoles.Add(g);
            context.ActorRoles.Add(h);
            context.ActorRoles.Add(i);
            context.ActorRoles.Add(j);
            context.ActorRoles.Add(k);
            context.ActorRoles.Add(l);
            context.Movies.Add(lotr);
            forum = new DiscussionForum { Movie = lotr };
            context.Forums.Add(forum);


            // casino royale - jeffrey
            Actor daniel = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/a/ab/Daniel_Craig_at_a_film_premiere_in_New_York.jpg", CopyrightMessage = "NYTrotter, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Daniel Craig", Bio = "One of the British theatre's most famous faces, Daniel Craig, who waited tables as a struggling teenage actor with the National Youth Theatre, has gone on to star as James Bond in Casino Royale (2006), Quantum of Solace (2008), Skyfall (2012), Spectre (2015), and No Time to Die (2021)." };
            context.Actors.Add(daniel);
            Actor eva = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/5/59/Eva_Green_%28Headshot%29.jpg", CopyrightMessage = "Dan Shao at https://www.flickr.com/people/70136517@N00, CC BY-SA 2.0 <https://creativecommons.org/licenses/by-sa/2.0>, via Wikimedia Commons", FullName = "Eva Green", Bio = "French actress and model Eva Gaëlle Green was born on July 6, 1980, in Paris, France. Her father, Walter Green, is a dentist who appeared in the 1966 film Au hasard Balthazar (1966). Her mother, Marlène Jobert, is an actress turned children's book writer. Eva's mother was born in Algeria, of French, Spanish, and Sephardi Jewish heritage (during that time, Algeria was part of France), and Eva's father is of Swedish, French, and Breton descent. She has a fraternal twin sister, Joy. Eva left French school at 17. She switched to the American School in France for one year. She left the American School and studied acting at Saint Paul Drama School in Paris for three years, then had a 10-week polishing course at the Weber Douglas Academy of dramatic Art in London. She returned to Paris as an accomplished young actress, and played on stage in several theater productions: 'La Jalousie en Trois Fax' and 'Turcaret'. There, she caught the eye of director Bernardo Bertolucci. Green followed a recommendation to work on her English. She studied for two months with an English coach before doing The Dreamers (2003) with Bernardo Bertolucci. During their work, Bertolucci described Green as being 'so beautiful it's indecent'." };
            context.Actors.Add(eva);
            Actor mads = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/d/d3/Mads_Mikkelsen_by_Gage_Skidmore.jpg", CopyrightMessage = "Gage Skidmore, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Mads Mikkelsen", Bio = "Mads Mikkelsen's great successes parallel those achieved by the Danish film industry since the mid-1990s. He was born in Østerbro, Copenhagen, to Bente Christiansen, a nurse, and Henning Mikkelsen, a banker." };
            context.Actors.Add(mads);
            Actor judi = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/a/ae/Judi_Dench_at_the_BAFTAs_2007.jpg", CopyrightMessage = "Caroline Bonarde Ucci, CC BY 3.0 <https://creativecommons.org/licenses/by/3.0>, via Wikimedia Commons", FullName = "Judi Dench", Bio = "Dame Judi Dench was born Judith Olivia Dench in York, England, to Eleanora Olive (Jones), who was from Dublin, Ireland, and Reginald Arthur Dench, a doctor from Dorset, England. She attended Mount School in York, and studied at the Central School of Speech and Drama. She has performed with the Royal Shakespeare Company, the National Theatre, and at Old Vic Theatre. She is a ten-time BAFTA winner including Best Actress in a Comedy Series for A Fine Romance (1981) in which she appeared with her husband, Michael Williams, and Best Supporting Actress in A Handful of Dust (1988) and A Room with a View (1985). She received an ACE award for her performance in the television series Star Quality: Mr. and Mrs. Edgehill (1985). She was appointed an Officer of the Order of the British Empire (OBE) in 1970, a Dame Commander of the Order of the British Empire (DBE) in 1988 and a Member of the Order of the Companions of Honour (CH) in 2005." };
            context.Actors.Add(judi);
            List<Actor> casinoCast = new List<Actor>
            {
                daniel,
                eva,
                mads,
                judi,
                jeffrey
            };
            List<Genre> casinoGenres = new List<Genre>
            {
                spy,
                action,
                romance
            };
            Movie casinoRoyale = new Movie
            {
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMDI5ZWJhOWItYTlhOC00YWNhLTlkNzctNDU5YTI1M2E1MWZhXkEyXkFqcGdeQXVyNTIzOTk5ODM@._V1_.jpg",
                CopyrightMessage = "",
                Title = "Casino Royale",
                Director = "Martin Campbell",
                Year = 2006,
                Cast = casinoCast,
                Genres = casinoGenres,
                Released = true,
                TrailerUrl = "https://www.youtube.com/embed/36mnx8dBbGE",
                Description = "Recently promoted to 00 status, James Bond (Daniel Craig) takes on his first mission, in which he faces a mysterious private banker to world terrorism and poker player, Le Chiffre (Mads Mikkelsen). Along with beautiful Treasury Agent Vesper Lynd (Eva Green) and the MI6 man in Montenegro, Bond takes part in a high stakes poker game set up by Le Chiffre in order to recover a huge sum of his clients' money he lost in a failed plot that the British spy took down. 007 will not only discover the threatening organization behind his enemy, but the worst of all truths: to not trust anyone."
            };
            a = new ActorRole { CharacterName = "James Bond/ 007", Actor = daniel, ActorId = daniel.ActorId, MovieId = casinoRoyale.MovieId, Movie = casinoRoyale };
            b = new ActorRole { CharacterName = "Vesper Lynd", Actor = eva, ActorId = eva.ActorId, MovieId = casinoRoyale.MovieId, Movie = casinoRoyale };
            c = new ActorRole { CharacterName = "Le Chiffre", Actor = mads, ActorId = mads.ActorId, MovieId = casinoRoyale.MovieId, Movie = casinoRoyale };
            d = new ActorRole { CharacterName = "M", Actor = judi, ActorId = judi.ActorId, MovieId = casinoRoyale.MovieId, Movie = casinoRoyale };
            e = new ActorRole { CharacterName = "Felix Leiter", Actor = jeffrey, ActorId = jeffrey.ActorId, MovieId = casinoRoyale.MovieId, Movie = casinoRoyale };

            context.ActorRoles.Add(a);
            context.ActorRoles.Add(b);
            context.ActorRoles.Add(c);
            context.ActorRoles.Add(d);
            context.ActorRoles.Add(e);
            context.Movies.Add(casinoRoyale);
            forum = new DiscussionForum { Movie = casinoRoyale };
            context.Forums.Add(forum);


            // dark shadows - depp, green, bonahmcarter, lee
            Actor michelle = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/1/19/Michelle_Pfeiffer_Ant-Man_%26_The_Wasp_premiere.jpg", CopyrightMessage = "joyparris, CC BY 3.0 <https://creativecommons.org/licenses/by/3.0>, via Wikimedia Commons", FullName = "Michelle Pffeifer", Bio = "Michelle Pfeiffer was born in Santa Ana, California to Dick and Donna Pfeiffer. She has an older brother and two younger sisters - Dedee Pfeiffer, and Lori Pfeiffer, who both dabbled in acting and modeling but decided against making it their lives' work. She graduated from Fountain Valley High School in 1976, and attended one year at the Golden West College, where she studied to become a court reporter. But it was while working as a supermarket checker at Vons, a large Southern California grocery chain, that she realized her true calling." };
            context.Actors.Add(michelle);
            List<Actor> dsCast = new List<Actor>
            {
                johnny,
                eva,
                michelle,
                helena,
                christopher
            };
            List<Genre> shadowsGenres = new List<Genre>
            {
                dcomedy,
                fantasy
            };
            Movie darkShadows = new Movie
            {
                PosterUrl = "https://upload.wikimedia.org/wikipedia/en/b/ba/Dark_Shadows_2012_Poster.jpg",
                CopyrightMessage = "",
                Title = "Dark Shadows",
                Director = "Tim Burton",
                Year = 2012,
                Genres = shadowsGenres,
                TrailerUrl = "https://www.youtube.com/embed/N6tVdffCr_M",
                Cast = dsCast,
                Released = true,
                Description = "Dark Shadows is a melodramatic comedy following the misfortunes of a vampire named Barnabas Collins. Centuries ago a witch had sought revenge upon the Collins family following her and the man's discontinued love affair. To his travail she set a curse on Barnabas turning him into a blood feeding monster and locking him away for years."
            };
            a = new ActorRole { CharacterName = "Barnabas Collins", Actor = johnny, ActorId = johnny.ActorId, MovieId = darkShadows.MovieId, Movie = darkShadows };
            b = new ActorRole { CharacterName = "Elizabeth Collins Stoddard", Actor = michelle, ActorId = michelle.ActorId, MovieId = darkShadows.MovieId, Movie = darkShadows };
            c = new ActorRole { CharacterName = "Angelique Bouchard", Actor = eva, ActorId = eva.ActorId, MovieId = darkShadows.MovieId, Movie = darkShadows };
            d = new ActorRole { CharacterName = "Dr. Julia Hoffman", Actor = helena, ActorId = helena.ActorId, MovieId = darkShadows.MovieId, Movie = darkShadows };
            e = new ActorRole { CharacterName = "Clarney", Actor = christopher, ActorId = christopher.ActorId, MovieId = darkShadows.MovieId, Movie = darkShadows };

            context.ActorRoles.Add(a);
            context.ActorRoles.Add(b);
            context.ActorRoles.Add(c);
            context.ActorRoles.Add(d);
            context.ActorRoles.Add(e);
            context.Movies.Add(darkShadows);
            forum = new DiscussionForum { Movie = darkShadows };
            context.Forums.Add(forum);


            // orient express - pffeifer, dench, depp
            Actor kenneth = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/c/c7/KennethBranaghApr2011.jpg", CopyrightMessage = "Melinda Seckington, CC BY 2.0 <https://creativecommons.org/licenses/by/2.0>, via Wikimedia Commons", FullName = "Kenneth Branagh", Bio = "Kenneth Charles Branagh was born on December 10, 1960, in Belfast, Northern Ireland, to parents William Branagh, a plumber and carpenter, and Frances (Harper), both born in 1930. He has two siblings, William Branagh, Jr. (born 1955) and Joyce Branagh (born 1970). When he was nine, his family escaped The Troubles by moving to Reading, Berkshire, England." };
            context.Actors.Add(kenneth);
            Actor daisy = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/6/6e/Daisy_Ridley_by_Gage_Skidmore.jpg", CopyrightMessage = "Gage Skidmore, CC BY-SA 3.0 <https://creativecommons.org/licenses/by-sa/3.0>, via Wikimedia Commons", FullName = "Daisy Ridley", Bio = "Daisy Jazz Isobel Ridley is an English actress. She is best known for her breakthrough role as Rey in the 2015 film, Star Wars: Episode VII - The Force Awakens (2015). Daisy was born in Westminster, London, on April 10, 1992. She is the daughter of Louise Fawkner-Corbett and Chris Ridley. Her great-uncle was Arnold Ridley, an English actor, playwright, and appointed Officer of the Order of the British Empire (OBE), who was best known for his authorship of the play, The Ghost Train, and his role as Private Godfrey in the British sitcom, Dad's Army (1968). Daisy attended the Tring Park School for Performing Arts, located in Hertfordshire, England, where she trained in musical theater and graduated in 2010, at the age of 18. Aside from acting, her talent repertoire includes ballet, jazz dancing, Latin American, and tap. Her vocal range is mezzo-soprano, where she is notably skilled in jazz and cabaret singing. Upon graduation, Daisy was hired in a number of roles in television, film, and music." };
            context.Actors.Add(daisy);
            Actor joshGad = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/8/82/Streamy_Awards_Photo_1306_%284513297847%29.jpg", CopyrightMessage = "The Bui Brothers, CC BY-SA 2.0 <https://creativecommons.org/licenses/by-sa/2.0>, via Wikimedia Commons", FullName = "Josh Gad", Bio = "Josh Gad was born on February 23, 1981 in Hollywood, Florida, USA. He is an actor and producer, known for Frozen (2013), Beauty and the Beast (2017) and Pixels (2015). He has been married to Ida Darvish since May 10, 2008. They have two children." };
            context.Actors.Add(joshGad);
            Actor penelope = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b7/Pen%C3%A9lope_Cruz_TIFF_2012.jpg", CopyrightMessage = "Tabercil, CC BY-SA 2.0 <https://creativecommons.org/licenses/by-sa/2.0>, via Wikimedia Commons", FullName = "Penelope Cruz", Bio = "Known outside her native country as the Spanish enchantress, Penélope Cruz Sánchez was born in Madrid to Eduardo Cruz, a retailer, and Encarna Sánchez, a hairdresser. As a toddler, she was already a compulsive performer, re-enacting TV commercials for her family's amusement, but she decided to focus her energies on dance. After studying classical ballet for nine years at Spain's National Conservatory, she continued her training under a series of prominent dancers. At 15, however, she heeded her true calling when she bested more than 300 other girls at a talent agency audition. The resulting contract landed her several roles in Spanish TV shows and music videos, which in turn paved the way for a career on the big screen. Cruz made her movie debut in The Greek Labyrinth (1993) (The Greek Labyrinth), then appeared briefly in the Timothy Dalton thriller Framed (1992). Her third film was the Oscar-winning Belle Epoque (1992), in which she played one of four sisters vying for the love of a handsome army deserter." };
            context.Actors.Add(penelope);
            Actor willem = new Actor { PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/7/76/WillemDafoeSept2011.jpg", CopyrightMessage = "Eva Rinaldi, CC BY-SA 2.0 <https://creativecommons.org/licenses/by-sa/2.0>, via Wikimedia Commons", FullName = "Willem Dafoe", Bio = "Having made over one hundred films in his legendary career, Willem Dafoe is internationally respected for bringing versatility, boldness, and daring to some of the most iconic films of our time. His artistic curiosity in exploring the human condition leads him to projects all over the world, large and small, Hollywood films as well as Independent cinema." };
            context.Actors.Add(willem);
            List<Actor> orientCast = new List<Actor>
            {
                kenneth,
                michelle,
                johnny,
                daisy,
                judi,
                penelope,
                willem,
                joshGad
            };
            List<Genre> orientGenres = new List<Genre>
            {
                crime,
                mistery,
                thriller
            };
            Movie orientExpress = new Movie
            {
                PosterUrl = "https://upload.wikimedia.org/wikipedia/en/b/bd/Murder_on_the_Orient_Express_teaser_poster.jpg",
                CopyrightMessage = "",
                Title = "Murder on the Orient Express",
                Director = "Kenneth Branagh",
                Year = 2017,
                Cast = orientCast,
                Genres = orientGenres,
                TrailerUrl = "https://www.youtube.com/embed/Mq4m3yAoW8E",
                Released = true,
                Description = "Famed Belgian detective Hercule Poirot is travelling on the Orient Express when a murder is committed. The victim was a particularly obnoxious man with a criminal past. As clues are revealed, it is apparent that many of the other passengers on the train had a motive to kill him, making Poirot's task of identifying the murderer much more difficult."
            };
            a = new ActorRole { CharacterName = "Hercule Poirot", Actor = kenneth, ActorId = kenneth.ActorId, MovieId = orientExpress.MovieId, Movie = orientExpress };
            b = new ActorRole { CharacterName = "Mary Debenham", Actor = daisy, ActorId = daisy.ActorId, MovieId = orientExpress.MovieId, Movie = orientExpress };
            c = new ActorRole { CharacterName = "Pilar Estravados", Actor = penelope, ActorId = penelope.ActorId, MovieId = orientExpress.MovieId, Movie = orientExpress };
            d = new ActorRole { CharacterName = "Edward Ratchett", Actor = johnny, ActorId = johnny.ActorId, MovieId = orientExpress.MovieId, Movie = orientExpress };
            e = new ActorRole { CharacterName = "Hector MacQueen", Actor = joshGad, ActorId = joshGad.ActorId, MovieId = orientExpress.MovieId, Movie = orientExpress };
            f = new ActorRole { CharacterName = "Caroline Hubbard", Actor = michelle, ActorId = michelle.ActorId, MovieId = orientExpress.MovieId, Movie = orientExpress };
            g = new ActorRole { CharacterName = "Princess Dragomiroff", Actor = judi, ActorId = judi.ActorId, MovieId = orientExpress.MovieId, Movie = orientExpress };
            h = new ActorRole { CharacterName = "Gerhard Hardman", Actor = willem, ActorId = willem.ActorId, MovieId = orientExpress.MovieId, Movie = orientExpress };

            context.ActorRoles.Add(a);
            context.ActorRoles.Add(b);
            context.ActorRoles.Add(c);
            context.ActorRoles.Add(d);
            context.ActorRoles.Add(e);
            context.ActorRoles.Add(f);
            context.ActorRoles.Add(g);
            context.ActorRoles.Add(h);
            context.Movies.Add(orientExpress);
            forum = new DiscussionForum { Movie = orientExpress };
            context.Forums.Add(forum);
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
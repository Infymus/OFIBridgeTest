using System.Text;

namespace Tests.Utilities
{
    public static class TestDataGenerator
    {

        // random defined 
        private static Random rnd = new Random();

        [Flags]
        public enum CharTypes
        {
            LowerCase = 0x0,
            UpperCase = 0x1,
            Numbers = 0x2,
            Punctuation = 0x4
        }

        private const string LOWERCASE = "abcdefghijklmnopqrstuvwxyz";
        private const string UPPERCASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string NUMBERS = "1234567890";
        private const string PUNCTUATION = "~`!@#$%^&*()_-+=}[|\\:;<,>.?/\"";

        public static string RandomChar(int numberChar, bool isInt = false, bool isAlpha = false)
        {
            Random r = new Random();

            // Determine character set based on flags
            string chars = isInt ? NUMBERS
                        : isAlpha ? UPPERCASE + LOWERCASE
                        : UPPERCASE + NUMBERS;

            // Generate the random string
            return new string(Enumerable.Range(0, numberChar)
                                        .Select(_ => chars[r.Next(chars.Length)])
                                        .ToArray());
        }

        // generates a random direction for addresses
        public static string GetRandomDirection()
        {
            string[] directions = { "North", "South", "East", "West" };
            int randIndex = rnd.Next(directions.Length);
            return directions[randIndex];
        }

        // generates a random string based on SIZE and uppper or lower
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * rnd.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            else
                return builder.ToString().ToUpper();
        }

        // generates a random number based on MIN and MAX 
        public static int RandomNumber(int min, int max)
        {
            return rnd.Next(min, max);
        }

        // generates a random true or false
        public static bool RandomBoolean()
        {
            int retRand = rnd.Next(1, 2);
            if (retRand == 1)
                return true;
            else
                return false;
        }

        // generates a random string with three sets of char types including lower, upper and number


        public static string RndStrGen(int aLength)
        {
            return RndStrGen(aLength, CharTypes.LowerCase | CharTypes.Numbers | CharTypes.UpperCase);
        }


        // generates a random string by length and char set
        public static string RndStrGen(int aLength, CharTypes aCharSet)
        {
            string result = string.Empty;

            string chars2Use = "";
            if ((aCharSet & CharTypes.LowerCase) == CharTypes.LowerCase)
                chars2Use += LOWERCASE;
            if ((aCharSet & CharTypes.Numbers) == CharTypes.Numbers)
                chars2Use += NUMBERS;
            if ((aCharSet & CharTypes.Punctuation) == CharTypes.Punctuation)
                chars2Use += PUNCTUATION;
            if ((aCharSet & CharTypes.UpperCase) == CharTypes.UpperCase)
                chars2Use += UPPERCASE;

            if (aLength > 0)
                for (int i = aLength; i > 0; i--)
                    result += chars2Use[rnd.Next(0, chars2Use.Length - 1)];

            return result;
        }

        // generates a random phone number 000-000-0000
        public static string GetRandomPhone(bool useDashes)
        {
            if (useDashes)
                return RandomNumber(100, 999).ToString() + "-" + RandomNumber(100, 999).ToString() + "-" + RandomNumber(1000, 9999).ToString();
            else
                return RandomNumber(100, 999).ToString() + RandomNumber(100, 999).ToString() + RandomNumber(1000, 9999).ToString();
        }

        // generates a random tax payer ID (social security number)
        public static string GetRandomTaxPayerID()
        {
            string taxId = "";
            for (int x = 1; x < 8; x++)
                taxId += RandomNumber(1, 9).ToString();
            return taxId;
        }

        public static String GetRandomDentalLicense()
        {
            int x;
            string drLic = "";
            for (int count = 1; count < 9; count++)
            {
                x = rnd.Next(1, 9);
                drLic += x.ToString();
            }
            drLic += RandomString(2, true);
            return drLic.ToUpper();
        }

        // generates a random first name
        public static string GetRandomFirstName()
        {
            string[] firstNames = {
                "James", "Mary", "John", "Patricia", "Robert", "Jennifer", "Michael", "Linda", "William",
                "Elizabeth", "David", "Barbara", "Richard", "Susan", "Joseph", "Jessica", "Thomas", "Sarah",
                "Charles", "Karen", "Christopher", "Nancy", "Daniel", "Lisa", "Matthew", "Betty", "Anthony",
                "Margaret", "Donald", "Sandra", "Mark", "Ashley", "Paul", "Kimberly", "Steven", "Donna",
                "Andrew", "Emily", "Kenneth", "Michelle", "Joshua", "Carol", "Kevin", "Amanda", "Brian",
                "Melissa", "George", "Deborah", "Edward", "Stephanie", "Ronald", "Rebecca", "Timothy",
                "Laura", "Jason", "Sharon", "Jeffrey", "Cynthia", "Ryan", "Kathleen", "Jacob", "Amy",
                "Gary", "Shirley", "Nicholas", "Angela", "Eric", "Helen", "Stephen", "Anna", "Jonathan",
                "Brenda", "Larry", "Pamela", "Justin", "Nicole", "Scott", "Ruth", "Brandon", "Katherine",
                "Benjamin", "Samantha", "Samuel", "Christine", "Gregory", "Emma", "Frank", "Catherine",
                "Alexander", "Debra", "Raymond", "Virginia", "Patrick", "Rachel", "Jack", "Janet", "Dennis",
                "Maria", "Jerry", "Heather", "Tyler", "Diane", "Aaron", "Julie", "Henry", "Joyce", "Douglas",
                "Victoria", "Peter", "Kelly", "Jose", "Christina", "Adam", "Lauren", "Zachary", "Joan",
                "Nathan", "Evelyn", "Walter", "Olivia", "Harold", "Judith"};

            int randIndex = rnd.Next(firstNames.Length);
            return firstNames[randIndex];
        }

        // generates a random last name
        public static string GetRandomLastName()
        {
            string[] lastNames = {
                "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Garcia", "Rodriguez",
                "Wilson", "Martinez", "Anderson", "Taylor", "Thomas", "Hernandez", "Moore", "Martin",
                "Jackson", "Thompson", "White", "Lopez", "Lee", "Gonzalez", "Harris", "Clark", "Lewis",
                "Robinson", "Walker", "Perez", "Hall", "Young", "Allen", "Sanchez", "Wright", "King",
                "Scott", "Green", "Baker", "Adams", "Nelson", "Hill", "Ramirez", "Campbell", "Mitchell",
                "Roberts", "Carter", "Phillips", "Evans", "Turner", "Torres", "Parker", "Collins", "Edwards",
                "Stewart", "Flores", "Morris", "Nguyen", "Murphy", "Rivera", "Cook", "Rogers", "Morgan",
                "Peterson", "Cooper", "Reed", "Bailey", "Bell", "Gomez", "Kelly", "Howard", "Ward",
                "Cox", "Diaz", "Richardson", "Wood", "Watson", "Brooks", "Bennett", "Gray", "James",
                "Reyes", "Cruz", "Hughes", "Price", "Myers", "Long", "Foster", "Sanders", "Ross", "Morales",
                "Powell", "Sullivan", "Russell", "Ortiz", "Jenkins", "Gutierrez", "Perry", "Butler",
                "Barnes", "Fisher"};

            int randIndex = rnd.Next(lastNames.Length);
            return lastNames[randIndex];
        }

        // generates a random password including length and punctuation
        public static String GetRandomPassword(bool usePunct, int pwLen)
        {
            string pw = "";
            int cnt;
            int roll = 0;
            //
            for (cnt = 1; cnt < pwLen; cnt++)
            {
                string Str;
                int rndNum;
                roll++;
                if (roll > 3)
                    roll = 1;
                Str = "";
                switch (roll)
                {
                    case 1: // alpha lower
                        rndNum = rnd.Next(1, 26) + 97;
                        Str = Char.ToString((char)rndNum);
                        break;
                    case 2: // numeric
                        rndNum = rnd.Next(1, 9) + 48;
                        Str = Char.ToString((char)rndNum);
                        break;
                    case 3: // alpha upper
                        rndNum = rnd.Next(1, 26) + 65;
                        Str = Char.ToString((char)rndNum);
                        break;
                }
                pw += Str;
                // punctuation 
                if (usePunct)
                {
                    rndNum = rnd.Next(1, 25);
                    #region Random PW Items
                    switch (rndNum)
                    {
                        case 1:
                            Str = "'";
                            break;
                        case 2:
                            Str = "#";
                            break;
                        case 3:
                            Str = "$";
                            break;
                        case 4:
                            Str = "%";
                            break;
                        case 5:
                            Str = "&";
                            break;
                        case 6:
                            Str = "'";
                            break;
                        case 7:
                            Str = "(";
                            break;
                        case 8:
                            Str = ")";
                            break;
                        case 9:
                            Str = "*";
                            break;
                        case 10:
                            Str = "+";
                            break;
                        case 11:
                            Str = "`";
                            break;
                        case 12:
                            Str = "~";
                            break;
                        case 13:
                            Str = "-";
                            break;
                        case 14:
                            Str = ".";
                            break;
                        case 15:
                            Str = "/";
                            break;
                        case 16:
                            Str = "^";
                            break;
                        case 17:
                            Str = "|";
                            break;
                        case 18:
                            Str = "[";
                            break;
                        case 19:
                            Str = "]";
                            break;
                        case 20:
                            Str = "{";
                            break;
                        case 21:
                            Str = "}";
                            break;
                        case 22:
                            Str = "=";
                            break;
                        case 23:
                            Str = "@";
                            break;
                        case 24:
                            Str = "^";
                            break;
                    }
                    #endregion
                    pw += Str;
                }
            }
            return pw;
        }

        // generates a Hipster Ipsum word
        public static string HipsterWord()
        {
            string[] hipsterWords = {
                "2", "3", "4", "5", "6", "7", "70's", "8", "8-bit", "80's", "9", "90''s", "Aesthetic",
                "American Apparel", "Anderson", "Austin", "Banksy", "Before they sold out", "Blue",
                "Blue Bottle", "Bottle", "Brooklyn", "Brunch", "Bushwick", "Carles", "Cliche", "Cosby",
                "Cosby", "DIY", "Echo", "Ennui", "Etsy", "Fixie", "Future", "Hella", "Helvetica", "IPhone",
                "Intelligentsia", "Kale", "Kickstarter", "Letterpress", "Marfa", "McSweeney''s", "Neutra",
                "Odd", "Odd Future", "PBR", "PBR&B", "Park", "Pitchfork", "Plaid", "Polaroid", "Pork belly",
                "Post-ironic", "Roof", "Schlitz", "Shoreditch", "Skateboard", "Thundercats", "Tonx",
                "Truffaut", "Tumblr", "Typewriter", "Umami", "VHS", "Vice", "Vice letterpress", "Vice retro",
                "Wes", "Wes Anderson", "Williamsburg", "XOXO", "YOLO", "aesthetic", "art", "artisan",
                "asymmetrical", "authentic", "axe", "bag", "banh", "banjo", "batch", "before they sold out",
                "belly", "blog", "brunch", "butcher", "cardigan", "chambray", "chia", "chips", "church-key",
                "cleanse", "craft beer", "cray", "cred", "deep", "direct", "direct trade", "distillery",
                "drinking", "ennui", "ennui Blue", "ethical", "ethnic", "six", "five", "farm-to-table",
                "fashion", "fixie", "flexitarian", "four", "four", "freegan", "gentrify", "gluten-free",
                "hoodie", "iPhone", "irony", "jean shorts", "kale", "keffiyeh", "keytar", "kitsch", "kogi",
                "kogi", "letterpress", "literally", "lo-fi", "locavore", "loko", "lomo", "master", "meggings",
                "meh", "messenger", "mi", "mixtape", "mlkshk", "moon", "moon", "mumblecore", "mustache",
                "neckbeard", "next level", "occupy", "paleo", "party", "photo booth", "poems", "pop-over",
                "pop-up", "pop-under", "pork", "post-ironic", "pour-over", "pug", "put a bird on it", "quinoa",
                "retro", "roof", "scenester", "seitan", "selfies", "selvage", "semiotics", "skateboard",
                "slow-carb", "small", "street", "street art", "stumptown", "swag", "sweater", "synth",
                "tattooed", "to this", "tofu", "tousled", "trade", "trust fund", "try-hard", "tweet", "ugh",
                "umami", "vegan", "vinegar", "viral", "wayfarers", "whatever", "wolf", "you probably haven't heard of them",
                "and", "then", "because", "or", "should have", "can't", "isn't", "was"};
            int randIndex = rnd.Next(hipsterWords.Length);
            return hipsterWords[randIndex];
        }

        // generates a Hipster Ipsum string by count
        public static string HipsterIpsum(int inCount)
        {
            int period = 0;
            int count;
            string hipster;
            int comma;
            string newHipster;
            string fir;
            hipster = "Hipster impsum";
            comma = 2;
            for (count = 1; count < inCount; count++)
            {
                newHipster = HipsterWord();
                if (period == 0)
                {
                    fir = newHipster.Substring(0, 1);
                    fir = fir.ToUpper();
                    newHipster = fir + newHipster.Substring(1, newHipster.Length - 1);
                }
                hipster += " " + HipsterWord();
                period++;
                comma++;
                if (period == 10)
                {
                    period = 0;
                    comma = 0;
                    hipster += ".";
                }
                if (comma > 5)
                {
                    comma = 0;
                    hipster += ",";
                }
            }
            //if copy(hipster, length(hipster), 1) <> '.' then
            hipster += ".";
            return hipster;
        }

        // This method brings back a random Lorem Ipsum Word
        public static string LoremWord()
        {
            string[] loremWords = {
                "sit", "amet", "consectetur", "adipiscing", "elit", "curabitur", "vel", "hendrerit", "libero",
                "eleifend", "blandit", "nunc", "ornare", "odio", "ut", "orci", "gravida", "imperdiet", "nullam",
                "purus", "lacinia", "a", "pretium", "quis", "congue", "praesent", "sagittis", "laoreet", "auctor",
                "mauris", "non", "velit", "eros", "dictum", "proin", "accumsan", "sapien", "nec", "massa", "volutpat",
                "venenatis", "sed", "eu", "molestie", "lacus", "quisque", "porttitor", "ligula", "dui", "mollis",
                "tempus", "at", "magna", "vestibulum", "turpis", "ac", "diam", "tincidunt", "id", "condimentum",
                "enim", "sodales", "in", "hac", "habitasse", "platea", "dictumst", "aenean", "neque", "fusce",
                "augue", "leo", "eget", "semper", "mattis", "tortor", "scelerisque", "nulla", "interdum", "tellus",
                "malesuada", "rhoncus", "porta", "sem", "aliquet", "et", "nam", "suspendisse", "potenti", "vivamus",
                "luctus", "fringilla", "erat", "donec", "justo", "vehicula", "ultricies", "varius", "ante", "primis",
                "faucibus", "ultrices", "posuere", "cubilia", "curae", "etiam", "cursus", "aliquam", "quam", "dapibus",
                "nisl", "feugiat", "egestas", "class", "aptent", "taciti", "sociosqu", "ad", "litora", "torquent",
                "per", "conubia", "nostra", "inceptos", "himenaeos", "phasellus", "nibh", "pulvinar", "vitae", "urna",
                "iaculis", "lobortis", "nisi", "viverra", "arcu", "morbi", "pellentesque", "metus", "commodo", "ut",
                "facilisis", "felis", "tristique", "ullamcorper", "placerat", "aenean", "convallis", "sollicitudin",
                "integer", "rutrum", "duis", "est", "etiam", "bibendum", "donec", "pharetra", "vulputate", "maecenas",
                "mi", "fermentum", "consequat", "suscipit", "aliquam", "habitant", "senectus", "netus", "fames",
                "quisque", "euismod", "curabitur", "lectus", "elementum", "tempor", "risus", "cras"};

            int randIndex = rnd.Next(loremWords.Length);
            return loremWords[randIndex];
        }

        // This method will return an entire string of Lorem Ipsum by inCount.
        public static string LoremIpsum(int inCount)
        {
            string lorem;
            int comma;
            int period = 0;
            int count;
            string fir;
            lorem = "Lorem impsum";
            comma = 2;
            for (count = 1; count < inCount; count++)
            {
                string newLorem = LoremWord();
                if (period == 0)
                {
                    fir = char.ToUpper(newLorem[0]) + newLorem.AsSpan(1).ToString();
                    newLorem = fir;
                }
                lorem += " " + newLorem;
                period++;
                comma++;
                if (period == 10)
                {
                    period = 0;
                    comma = 0;
                    lorem += ".";
                }
                if (comma > 5)
                {
                    comma = 0;
                    lorem += ",";
                }
            }
            lorem += ".";
            return lorem;
        }

        // Randomly generates an address 
        public static string GetRandomAddress()
        {
            return RandomNumber(1000, 9999).ToString() + " " + GetRandomDirection() + " " +
                RandomNumber(1000, 9999).ToString() + " " + GetRandomDirection();
        }

        // Randomly generates a zip code
        public static string GetRandomZipCode()
        {
            return RandomNumber(10000, 89999).ToString();
        }

        // randomly generates an email
        public static string GetRandomEmail()
        {
            return RandomString(15, true) + "@" + RandomString(10, false) + ".com";
        }

        // generates a random company email 
        public static String GenerateEmailBySalesType(string inEmail)
        {
            string[] salesTypes = { "Sales", "Marketing", "Contact" };
            string propType = salesTypes[rnd.Next(salesTypes.Length)];

            return $"{propType}@{inEmail}.com";
        }

        // generates a random email company address
        public static string GetRandomEmailCompany(string inEmailName, string inCompanyName, bool useRandCompany = false)
        {
            string randEmail = "";
            if (useRandCompany)
                randEmail = inEmailName + "@" + GetRandomCompanyName() + ".com";

            else
                //randEmail = inEmailName + "@" + inCompanyName + ".com";
                randEmail = $"{inEmailName}@{inCompanyName}.com";
            return randEmail.Replace(" ", string.Empty).ToLower();
        }

        // generates randome property types
        public static String GeneratePropertyType()
        {
            string[] propertyTypes = { "Retail", "Office", "Industrial" };
            int randIndex = rnd.Next(propertyTypes.Length);
            return propertyTypes[randIndex];
        }

        // generates a random driver license
        public static String GetRandomDriverLicense()
        {
            int x;
            string drLic = "";
            for (int count = 1; count < 9; count++)
            {
                x = rnd.Next(1, 9);
                drLic += x.ToString();
            }
            drLic += RandomString(2, true);
            return drLic.ToUpper();
        }

        // generates a random date of birth with MM DD YY between 18-60
        public static string GetRandomDateofBirth()
        {
            // Generate a random age between 18 and 60
            int age = rnd.Next(18, 60);

            // Calculate the year of birth
            int year = DateTime.Now.Year - age;

            // Generate a random month and day
            int month = rnd.Next(1, 13); // Month range is 1 to 12
            int day = rnd.Next(1, DateTime.DaysInMonth(year, month) + 1); // Day range adjusted for month and year

            // Create a DateTime object and format it as "MM/dd/yyyy"
            DateTime dateOfBirth = new DateTime(year, month, day);
            return dateOfBirth.ToString("MM/dd/yyyy");
        }

        // generates a random State
        public static string GetRandomState()
        {
            string[] stateNames = {
                "AK", "AS", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FM", "FL", "GA", "GU", "HI", "ID",
                "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MH", "MD", "MA", "MI", "MN", "MS", "MO", "MT",
                "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "MP", "OH", "OK", "OR", "PW", "PA", "PR",
                "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VI", "VA", "WA", "WV", "WI", "WY"};

            int idx = rnd.Next(stateNames.Length);
            return stateNames[idx];
        }

        // generates a random company name
        public static string GetRandomCompanyName()
        {
            string[] companyFirstNames = {
                "Widget", "Demo", "Anca", "Foo", "ABC", "Fake", "Qwerty", "Demo", "Sample", "Extensive",
                "North", "South", "East", "West", "Cogindu", "Colescent", "Colia", "Comill", "Conic",
                "Contrava", "Corava", "Corose", "Cryolith", "Cryptodel", "Delane", "Demitz", "Demozu",
                "Diazio", "Dietz", "Difore", "Dimbu", "Disible", "Divava", "Dolil", "Dominize", "Domive",
                "Dumia", "Duogen", "Dynazu", "Dynonix", "Eambo", "Ecombo", "Eideo", "Electrombu", "Enor",
                "Epifix", "Equnu", "Esill", "Eurova", "Execucero", "Exicious", "Fadoo", "Falcor", "Felist",
                "Finescent", "Fligen", "Foric", "Fortous", "Frontosis", "Garous", "Genible", "Genix",
                "Geonder", "Gradivee", "Hemizu", "Homolith", "Hydrotri", "Hypericious", "Hypodeo", "Ideonte",
                "Idiondo", "Infrava", "Inous", "Insulicious", "Interil", "Intralia", "Isota", "Jaloo",
                "Kadeo", "Kayism", "Kifix", "Kwifix", "Ladeo", "Laudend", "Leedo", "Locent", "Lunosis",
                "Lupive", "Malose", "Manunu", "Marent", "Maxify", "Medizz", "Meenti", "Megape", "Metado",
                "Micronix", "Midible", "Misend", "Mistinder", "Mizz", "Monosis", "Movoid", "Mulith",
                "Multify", "Myosis", "Neombu", "Nonist", "Nymba", "Octonix", "Ofy", "Omnimia", "Outent",
                "Oyofix", "Paleofy", "Panor", "Paramm", "Pedive", "Perill", "Perivu", "Pixoyo", "Plajo",
                "Podoid", "Polyile", "Portic", "Postive", "Premible", "Prezu", "Prondo", "Prosaria",
                "Protolium", "Pyrojo", "Quasilium", "Quayo", "Quimbu", "Refic", "Retrofix", "Rhyic",
                "Roodel", "Scidel", "Semilium", "Skadel", "Skitude", "Skyise", "Socicy", "Subist", "Sucor",
                "Sufize", "Sugise", "Sumible", "Superil", "Supism", "Supranyx", "Surible", "Surore",
                "Susible", "Syer", "Sylive", "Symescent", "Synism", "Sysent", "Tadeo", "Teleloo", "Tenible",
                "Transic", "Trideo", "Truzz", "Twire", "Twixter", "Uberoid", "Ultraveo", "Unescent",
                "Unimbu", "Venise", "Verist", "Vertible", "Vicefix", "Vilane", "Vivill", "Voore", "Wikiyo",
                "Yakiveo", "Yanu", "Yondo", "Zafy", "Zoodoo"};

            string[] companyLastNames = {
                "Holdings", "Inc.", "Storage", "Logistics", "Enterprises", "Brothers", "Institute",
                "Transcontinental", "Systems", "Warehousing", "Products", "Ltd", "and Foundry", "Co",
                "Company", "Solutions", "Compactions", "Aggregators", "Slummers", "Information Technologies",
                "Consortium", "Innovations"};

            int FirstIndex = rnd.Next(companyFirstNames.Length);
            int SecondIndex = rnd.Next(companyLastNames.Length);

            return companyFirstNames[FirstIndex] + " " + companyLastNames[SecondIndex];
        }

        // generates a random city name based on Utah cities
        public static string GetRandomCity()
        {
            string[] cityNames = {
                "Altamont", "Alton", "Altonah", "American Fork", "Aneth", "Annabella", "Antimony", "Aurora",
                "Axtell", "Bear River City", "Beaver", "Beryl", "Bicknell", "Bingham Canyon", "Blanding",
                "Bluebell", "Bluff", "Bonanza", "Boulder", "Bountiful", "Brian Head", "Brigham City", "Bryce",
                "Bryce Canyon", "Cache Junction", "Cannonville", "Castle Dale", "Cedar City", "Cedar Valley",
                "Centerfield", "Centerville", "Central", "Chester", "Circleville", "Cisco", "Clarkston",
                "Clawson", "Clearfield", "Cleveland", "Clinton", "Coalville", "Collinston", "Corinne", "Cornish",
                "Croydon", "Dammeron Valley", "Delta", "Deweyville", "Draper", "Duchesne", "Duck Creek Village",
                "Dugway", "Dutch John", "East Carbon", "Echo", "Eden", "Elberta", "Elmo", "Elsinore", "Emery",
                "Enterprise", "Ephraim", "Escalante", "Eureka", "Fairview", "Farmington", "Fayette", "Ferron",
                "Fielding", "Fillmore", "Fort Duchesne", "Fountain Green", "Fruitland", "Garden City", "Garland",
                "Garrison", "Glendale", "Glenwood", "Goshen", "Grantsville", "Green River", "Greenville",
                "Greenwich", "Grouse Creek", "Gunlock", "Gunnison", "Gusher", "Hanksville", "Hanna", "Hatch",
                "Heber City", "Helper", "Henefer", "Henrieville", "Hiawatha", "Hildale", "Hill AFB", "Hinckley",
                "Holden", "Honeyville", "Hooper", "Howell", "Huntington", "Huntsville", "Hurricane", "Hyde Park",
                "Hyrum", "Ibapah", "Ivins", "Jensen", "Joseph", "Junction", "Kamas", "Kanab", "Kanarraville",
                "Kanosh", "Kaysville", "Kenilworth", "Kingston", "Koosharem", "La Sal", "La Verkin", "Lake Powell",
                "Laketown", "Lapoint", "Layton", "Leamington", "Leeds", "Lehi", "Levan", "Lewiston", "Lindon",
                "Loa", "Logan", "Lyman", "Lynndyl", "Magna", "Manila", "Manti", "Mantua", "Mapleton",
                "Marysvale", "Mayfield", "Meadow", "Mendon", "Mexican Hat", "Midvale", "Midway", "Milford",
                "Millville", "Minersville", "Moab", "Modena", "Mona", "Monroe", "Montezuma Creek", "Monticello",
                "Morgan", "Moroni", "Mount Carmel", "Mount Pleasant", "Mountain Home", "Myton", "Neola", "Nephi",
                "New Harmony", "Newcastle", "Newton", "North Salt Lake", "Oak City", "Oakley", "Oasis", "Ogden",
                "Orangeville", "Orderville", "Orem", "Panguitch", "Paradise", "Paragonah", "Park City", "Park Valley",
                "Parowan", "Payson", "Peoa", "Pine Valley", "Pleasant Grove", "Plymouth", "Portage", "Price",
                "Providence", "Provo", "Randlett", "Randolph", "Redmond", "Richfield", "Richmond", "Riverside",
                "Riverton", "Rockville", "Roosevelt", "Roy", "Rush Valley", "Saint George", "Salem", "Salina",
                "Salt Lake City", "Sandy", "Santa Clara", "Santaquin", "Scipio", "Sevier", "Sigurd", "Smithfield",
                "Snowville", "South Jordan", "South Willard", "Spanish Fork", "Spring City", "Springdale",
                "Springville", "Sterling", "Stockton", "Summit", "Sunnyside", "Syracuse", "Tabiona", "Talmage",
                "Teasdale", "Thompson", "Tooele", "Toquerville", "Torrey", "Tremonton", "Trenton", "Tridell",
                "Tropic", "Vernal", "Vernon", "Veeyo", "Virgin", "Wales", "Washington", "Wellsburg", "Wellington",
                "Wellsville", "East Wendover", "East Jordan", "Whiterocks", "Willard", "Woodgruff", "Woods Cross"};

            int randIndex = rnd.Next(cityNames.Length);
            return cityNames[randIndex];
        }
    }
}

using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data
{
    public static class ProductContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<Product> productCollection)
        {
            if (!await productCollection.Find(b => true).AnyAsync())
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        Id = "602d2149e773f2a3990b47f5",
                        Name = "Adidas Quick Force Indoor Badminton Shoes",
                        Description = "Designed for professional as well as amateur badminton players. These indoor shoes are crafted with synthetic upper that provides natural fit, while the EVA midsole provides lightweight cushioning. The shoes can be used for Badminton and Squash",
                        Price = 3500,
                        ImageFile = "images/products/adidas_shoe-1.png",
                        Summary = "Adidas Quick Force Indoor Badminton Shoes",
                        Types = new ProductType { Id = "63ca5d4bc3a8a58f47299f97", Name = "Shoes" },
                        Brands = new ProductBrand { Id = "63ca5e40e0aa3968b549af53", Name = "Adidas" }
                    },
                    new Product
                    {
                        Id = "602d2149e773f2a3990b47f6",
                        Name = "Adidas Quick Force Indoor Badminton Shoes",
                        Description = "Designed for professional as well as amateur badminton players. These indoor shoes are crafted with synthetic upper that provides natural fit, while the EVA midsole provides lightweight cushioning. The shoes can be used for Badminton and Squash",
                        Price = 3375,
                        ImageFile = "images/products/adidas_shoe-2.png",
                        Summary = "Adidas Quick Force Indoor Badminton Shoes",
                        Types = new ProductType { Id = "63ca5d4bc3a8a58f47299f97", Name = "Shoes" },
                        Brands = new ProductBrand { Id = "63ca5e40e0aa3968b549af53", Name = "Adidas" }
                    },
                    new Product
                    {
                        Id = "602d2149e773f2a3990b47f7",
                        Name = "Adidas Quick Force Indoor Badminton Shoes",
                        Description = "Designed for professional as well as amateur badminton players. These indoor shoes are crafted with synthetic upper that provides natural fit, while the EVA midsole provides lightweight cushioning. The shoes can be used for Badminton and Squash",
                        Price = 3375,
                        ImageFile = "images/products/adidas_shoe-3.png",
                        Summary = "Adidas Quick Force Indoor Badminton Shoes",
                        Types = new ProductType { Id = "63ca5d4bc3a8a58f47299f97", Name = "Shoes" },
                        Brands = new ProductBrand { Id = "63ca5e40e0aa3968b549af53", Name = "Adidas" }
                    },
                    new Product
                    {
                        Id = "602d2149e773f2a3990b47f8",
                        Name = "Asics Gel Rocket 8 Indoor Court Shoes",
                        Description = "The Asics Gel Rocket 8 Indoor Court Shoes (Orange/Silver) will keep you motivated and fired up to perform at your peak in volleyball, squash and other indoor sports. Beginner and intermediate players get cutting-edge technologies at an affordable price point.This entry level all-rounder has a durable upper and offers plenty of stability.",
                        Price = 4249,
                        ImageFile = "images/products/asics_shoe-1.png",
                        Summary = "Asics Gel Rocket 8 Indoor Court Shoes",
                        Types = new ProductType { Id = "63ca5d4bc3a8a58f47299f97", Name = "Shoes" },
                        Brands = new ProductBrand { Id = "63ca5e4c455900b990b43bc1", Name = "ASICS" }
                    },
                    new Product
                    {
                        Id = "602d2149e773f2a3990b47f9",
                        Name = "Asics Gel Rocket 8 Indoor Court Shoes",
                        Description = "The Asics Gel Rocket 8 Indoor Court Shoes (Orange/Silver) will keep you motivated and fired up to perform at your peak in volleyball, squash and other indoor sports. Beginner and intermediate players get cutting-edge technologies at an affordable price point.This entry level all-rounder has a durable upper and offers plenty of stability.",
                        Price = 3499,
                        ImageFile = "images/products/asics_shoe-2.png",
                        Summary = "Asics Gel Rocket 8 Indoor Court Shoes",
                        Types = new ProductType { Id = "63ca5d4bc3a8a58f47299f97", Name = "Shoes" },
                        Brands = new ProductBrand { Id = "63ca5e4c455900b990b43bc1", Name = "ASICS" }
                    },
                    new Product
                    {
                        Id = "602d2149e773f2a3990b47fa",
                        Name = "Asics Gel Rocket 8 Indoor Court Shoes",
                        Description = "The Asics Gel Rocket 8 Indoor Court Shoes (Orange/Silver) will keep you motivated and fired up to perform at your peak in volleyball, squash and other indoor sports. Beginner and intermediate players get cutting-edge technologies at an affordable price point.This entry level all-rounder has a durable upper and offers plenty of stability.",
                        Price = 3499,
                        ImageFile = "images/products/asics_shoe-3.png",
                        Summary = "Asics Gel Rocket 8 Indoor Court Shoes",
                        Types = new ProductType { Id = "63ca5d4bc3a8a58f47299f97", Name = "Shoes" },
                        Brands = new ProductBrand { Id = "63ca5e4c455900b990b43bc1", Name = "ASICS" }
                    },
                    new Product
                    {
                        Id = "702d2149e773f2a3990b47fa",
                        Name = "Victor SHW503 F Badminton Shoes",
                        Description = "PU Leather, Mesh,EVA, ENERGY MAX, Nylon sheet, Rubber",
                        Price = 2392,
                        ImageFile = "images/products/victor_shoe-1.png",
                        Summary = "Victor SHW503 F Badminton Shoes",
                        Types = new ProductType { Id = "63ca5d4bc3a8a58f47299f97", Name = "Shoes" },
                        Brands = new ProductBrand { Id = "63ca5e59065163c16451bd73", Name = "Victor" }
                    },
                    new Product
                    {
                        Id = "802d2149e773f2a3990b47fa",
                        Name = "Victor SHW503 F Badminton Shoes",
                        Description = "PU Leather, Mesh,EVA, ENERGY MAX, Nylon sheet, Rubber",
                        Price = 3000,
                        ImageFile = "images/products/victor_shoe-2.png",
                        Summary = "Victor SHW503 F Badminton Shoes",
                        Types = new ProductType { Id = "63ca5d4bc3a8a58f47299f97", Name = "Shoes" },
                        Brands = new ProductBrand { Id = "63ca5e59065163c16451bd73", Name = "Victor" }
                    },
                    new Product
                    {
                        Id = "902d2149e773f2a3990b47fa",
                        Name = "Yonex Super Ace Light Badminton Shoes",
                        Description = "Rule the game with Super Ace Light highlights Maximum shock absorption Quick compression recovery Its high resilience ensure YONEX insoles retain their shape longer Meticulously contoured for comfort Provides stability in the forefoot and toe areas technology.",
                        Price = 2590,
                        ImageFile = "images/products/yonex_shoe-1.png",
                        Summary = "Yonex Super Ace Light Badminton Shoes",
                        Types = new ProductType { Id = "63ca5d4bc3a8a58f47299f97", Name = "Shoes" },
                        Brands = new ProductBrand { Id = "63ca5e655ec1fdc49bd9327d", Name = "Yonex" }
                    },
                    new Product
                    {
                        Id = "912d2149e773f2a3990b47fa",
                        Name = "Yonex Super Ace Light Badminton Shoes",
                        Description = "Rule the game with Super Ace Light highlights Maximum shock absorption Quick compression recovery Its high resilience ensure YONEX insoles retain their shape longer Meticulously contoured for comfort Provides stability in the forefoot and toe areas technology.",
                        Price = 3500,
                        ImageFile = "images/products/yonex_shoe-2.png",
                        Summary = "Yonex Super Ace Light Badminton Shoes",
                        Types = new ProductType { Id = "63ca5d4bc3a8a58f47299f97", Name = "Shoes" },
                        Brands = new ProductBrand { Id = "63ca5e655ec1fdc49bd9327d", Name = "Yonex" }
                    },
                    new Product
                    {
                        Id = "922d2149e773f2a3990b47fa",
                        Name = "Yonex Super Ace Light Badminton Shoes",
                        Description = "Rule the game with Super Ace Light highlights Maximum shock absorption Quick compression recovery Its high resilience ensure YONEX insoles retain their shape longer Meticulously contoured for comfort Provides stability in the forefoot and toe areas technology.",
                        Price = 3700,
                        ImageFile = "images/products/yonex_shoe-3.png",
                        Summary = "Yonex Super Ace Light Badminton Shoes",
                        Types = new ProductType { Id = "63ca5d4bc3a8a58f47299f97", Name = "Shoes" },
                        Brands = new ProductBrand { Id = "63ca5e655ec1fdc49bd9327d", Name = "Yonex" }
                    },
                    new Product
                    {
                        Id = "932d2149e773f2a3990b47fa",
                        Name = "Puma 19 FH Rubber Spike Cricket Shoes",
                        Description = "With features and functions designed to withstand long hours out on the pitch, these one8 19 FH Rubber shoes have been engineered to offer comfort to cricketers",
                        Price = 4999,
                        ImageFile = "images/products/puma_shoe-1.png",
                        Summary = "Puma 19 FH Rubber Spike Cricket Shoes",
                        Types = new ProductType { Id = "63ca5d4bc3a8a58f47299f97", Name = "Shoes" },
                        Brands = new ProductBrand { Id = "63ca5e728c4cff9708ada2a6", Name = "Puma" }
                    },
                    new Product
                    {
                        Id = "942d2149e773f2a3990b47fa",
                        Name = "Puma Super Ace Light Badminton Shoes",
                        Description = "With features and functions designed to withstand long hours out on the pitch, these one8 19 FH Rubber shoes have been engineered to offer comfort to cricketers.",
                        Price = 5200,
                        ImageFile = "images/products/puma_shoe-2.png",
                        Summary = "Puma Super Ace Light Badminton Shoes",
                        Types = new ProductType { Id = "63ca5d4bc3a8a58f47299f97", Name = "Shoes" },
                        Brands = new ProductBrand { Id = "63ca5e728c4cff9708ada2a6", Name = "Puma" }
                    },
                    new Product
                    {
                        Id = "952d2149e773f2a3990b47fa",
                        Name = "Puma 19 FH Rubber Spike Cricket Shoes",
                        Description = "With features and functions designed to withstand long hours out on the pitch, these one8 19 FH Rubber shoes have been engineered to offer comfort to cricketers.",
                        Price = 5700,
                        ImageFile = "images/products/puma_shoe-3.png",
                        Summary = "Puma 19 FH Rubber Spike Cricket Shoes",
                        Types = new ProductType { Id = "63ca5d4bc3a8a58f47299f97", Name = "Shoes" },
                        Brands = new ProductBrand { Id = "63ca5e728c4cff9708ada2a6", Name = "Puma" }
                    },
                    new Product
                    {
                        Id = "962d2149e773f2a3990b47fa",
                        Name = "Babolat Shadow Spirit Mens Badminton Shoes (Cloisonne/Strike)",
                        Description = "Babolat Shadow Spirit Mens Badminton Shoes (Cloisonne/Strike)",
                        Price = 4125,
                        ImageFile = "images/products/babolat_shoe-1.png",
                        Summary = "Babolat Shadow Spirit Mens Badminton Shoes (Cloisonne/Strike)",
                        Types = new ProductType { Id = "63ca5d4bc3a8a58f47299f97", Name = "Shoes" },
                        Brands = new ProductBrand { Id = "63ca5e8d6110a9c56ee7dc48", Name = "Babolat" }
                    },
                    new Product
                    {
                        Id = "972d2149e773f2a3990b47fa",
                        Name = "Babolat Shadow Tour Mens Badminton Shoes (White/Blue)",
                        Description = "Babolat Shadow Tour Mens Badminton Shoes (White/Blue)",
                        Price = 5249,
                        ImageFile = "images/products/babolat_shoe-2.png",
                        Summary = "Babolat Shadow Tour Mens Badminton Shoes (White/Blue)",
                        Types = new ProductType { Id = "63ca5d4bc3a8a58f47299f97", Name = "Shoes" },
                        Brands = new ProductBrand { Id = "63ca5e8d6110a9c56ee7dc48", Name = "Babolat" }
                    },
                    new Product
                    {
                        Id = "982d2149e773f2a3990b47fa",
                        Name = "Babolat Shadow Team Womens Badminton Shoes (Black/Peony)",
                        Description = "Babolat Shadow Team Womens Badminton Shoes (Black/Peony)",
                        Price = 2999,
                        ImageFile = "images/products/babolat_shoe-3.png",
                        Summary = "Babolat Shadow Team Womens Badminton Shoes (Black/Peony)",
                        Types = new ProductType { Id = "63ca5d4bc3a8a58f47299f97", Name = "Shoes" },
                        Brands = new ProductBrand { Id = "63ca5e8d6110a9c56ee7dc48", Name = "Babolat" }
                    },
                    new Product
                    {
                        Id = "983d2149e773f2a3990b47fa",
                        Name = "Yonex VCORE Pro 100 A Tennis Racquet (270gm, Strung)",
                        Description = "For offensive players looking to win with game-changing spin and power.",
                        Price = 6099,
                        ImageFile = "images/products/yonex-racket-1.png",
                        Summary = "Yonex VCORE Pro 100 A Tennis Racquet (270gm, Strung)",
                        Types = new ProductType { Id = "63ca5d6d958e43ee1cd375fe", Name = "Rackets" },
                        Brands = new ProductBrand { Id = "63ca5e655ec1fdc49bd9327d", Name = "Yonex" }
                    },
                    new Product
                    {
                        Id = "991d2149e773f2a3990b47fa",
                        Name = "Yonex VCORE Pro 100 A Tennis Racquet (290gm, Strung)",
                        Description = "For offensive players looking to win with game-changing spin and power.",
                        Price = 6399,
                        ImageFile = "images/products/yonex-racket-2.png",
                        Summary = "Yonex VCORE Pro 100 A Tennis Racquet (290gm, Strung)",
                        Types = new ProductType { Id = "63ca5d6d958e43ee1cd375fe", Name = "Rackets" },
                        Brands = new ProductBrand { Id = "63ca5e655ec1fdc49bd9327d", Name = "Yonex" }
                    },
                    new Product
                    {
                        Id = "992d2149e773f2a3990b47fa",
                        Name = "Yonex VCORE Pro 100 2019 Tennis Racquet (280gm, Unstrung)",
                        Description = "For offensive players looking to win with game-changing spin and power.",
                        Price = 13299,
                        ImageFile = "images/products/yonex-racket-3.png",
                        Summary = "Yonex VCORE Pro 100 A Tennis Racquet (290gm, Strung)",
                        Types = new ProductType { Id = "63ca5d6d958e43ee1cd375fe", Name = "Rackets" },
                        Brands = new ProductBrand { Id = "63ca5e655ec1fdc49bd9327d", Name = "Yonex" }
                    },
                    new Product
                    {
                        Id = "993d2149e773f2a3990b47fa",
                        Name = "Babolat Boost D Tennis Racquet (260gm, Strung)",
                        Description = "Babolat Boost D Tennis Racquet (260gm, Strung)",
                        Price = 6999,
                        ImageFile = "images/products/babolat-racket-1.png",
                        Summary = "Babolat Boost D Tennis Racquet (260gm, Strung)",
                        Types = new ProductType { Id = "63ca5d6d958e43ee1cd375fe", Name = "Rackets" },
                        Brands = new ProductBrand { Id = "63ca5e8d6110a9c56ee7dc48", Name = "Babolat" }
                    },
                    new Product
                    {
                        Id = "994d2149e773f2a3990b47fa",
                        Name = "Babolat Pure Aero 2019 Superlite Tennis Racquet (Unstrung, 255gm)",
                        Description = "Buy Babolat Pure Aero 2019 Superlite Tennis Racquet (Unstrung, 255gm) and get Free Stringing with Babolat RPM Blast String",
                        Price = 13000,
                        ImageFile = "images/products/babolat-racket-2.png",
                        Summary = "Babolat Pure Aero 2019 Superlite Tennis Racquet",
                        Types = new ProductType { Id = "63ca5d6d958e43ee1cd375fe", Name = "Rackets" },
                        Brands = new ProductBrand { Id = "63ca5e8d6110a9c56ee7dc48", Name = "Babolat" }
                    },
                    new Product
                    {
                        Id = "995d2149e773f2a3990b47fa",
                        Name = "Babolat Pure Drive VS Tennis Racquet (Pair, 300gm, Strung)",
                        Description = "Babolat Pure Drive VS Tennis Racquet (Pair, 300gm, Strung)",
                        Price = 34000,
                        ImageFile = "images/products/babolat-racket-3.png",
                        Summary = "Babolat Pure Drive VS Tennis Racquet (Pair, 300gm, Strung)",
                        Types = new ProductType { Id = "63ca5d6d958e43ee1cd375fe", Name = "Rackets" },
                        Brands = new ProductBrand { Id = "63ca5e8d6110a9c56ee7dc48", Name = "Babolat" }
                    },
                    new Product
                    {
                        Id = "996d2149e773f2a3990b47fa",
                        Name = "Adidas FIFA World Cup 2018 OMB Football (White/Red/Black)",
                        Description = "Featuring an innovative surface panel design, this is the match ball used during football's FIFA World Cup™. Inspired by Russia's urban landscapes, a pixelated graphic pays tribute to the iconic Telstar ball. Its thermally bonded seamless surface designs.",
                        Price = 2499,
                        ImageFile = "images/products/adidas_football-1.png",
                        Summary = "Adidas FIFA World Cup 2018 OMB Football (White/Red/Black)",
                        Types = new ProductType { Id = "63ca5d7d380402dce7f06ebc", Name = "Football" },
                        Brands = new ProductBrand { Id = "63ca5e40e0aa3968b549af53", Name = "Adidas" }
                    },
                    new Product
                    {
                        Id = "997d2149e773f2a3990b47fa",
                        Name = "Adidas FIFA World Cup 2018 OMB Football",
                        Description = "Featuring an innovative surface panel design, this is the match ball used during football's FIFA World Cup™. Inspired by Russia's urban landscapes, a pixelated graphic pays tribute to the iconic Telstar ball. Its thermally bonded seamless surface designs.",
                        Price = 3200,
                        ImageFile = "images/products/adidas_football-2.png",
                        Summary = "Adidas FIFA World Cup 2018 OMB Football",
                        Types = new ProductType { Id = "63ca5d7d380402dce7f06ebc", Name = "Football" },
                        Brands = new ProductBrand { Id = "63ca5e40e0aa3968b549af53", Name = "Adidas" }
                    },
                    new Product
                    {
                        Id = "998d2149e773f2a3990b47fa",
                        Name = "Adidas FIFA World Cup Top Glider Ball",
                        Description = "Featuring an innovative surface panel design, this is the match ball used during football's FIFA World Cup™. Inspired by Russia's urban landscapes, a pixelated graphic pays tribute to the iconic Telstar ball. Its thermally bonded seamless surface designs.",
                        Price = 2499,
                        ImageFile = "images/products/adidas_football-3.png",
                        Summary = "Adidas FIFA World Cup 2023 OMB Football",
                        Types = new ProductType { Id = "63ca5d7d380402dce7f06ebc", Name = "Football" },
                        Brands = new ProductBrand { Id = "63ca5e40e0aa3968b549af53", Name = "Adidas" }
                    },
                    new Product
                    {
                        Id = "999d2149e773f2a3990b47fa",
                        Name = "Nike Pitch Premier League Football (Yellow/Purple)",
                        Description = "Nike Pitch Premier League Football (Yellow/Purple) Ball is made with colorful graphics that stand out on the field for easier ball tracking. A machine-stitched TPU casing delivers great touch and durability during play. ",
                        Price = 1525,
                        ImageFile = "images/products/Nike-Football-1.png",
                        Summary = "Nike Pitch Premier League Football (Yellow/Purple)",
                        Types = new ProductType { Id = "63ca5d7d380402dce7f06ebc", Name = "Football" },
                        Brands = new ProductBrand { Id = "63ca5e7ec90ff5c8f44d5ac8", Name = "Nike" }
                    },
                    new Product
                    {
                        Id = "99912149e773f2a3990b47fa",
                        Name = "Nike Manchester City Supporters Football (Berry)",
                        Description = "Nike Manchester City Supporters Football (Berry) Ball is made with colorful graphics that stand out on the field for easier ball tracking. A machine-stitched TPU casing delivers great touch and durability during play. ",
                        Price = 1525,
                        ImageFile = "images/products/Nike-Football-2.png",
                        Summary = "Nike Manchester City Supporters Football (Berry)",
                        Types = new ProductType { Id = "63ca5d7d380402dce7f06ebc", Name = "Football" },
                        Brands = new ProductBrand { Id = "63ca5e7ec90ff5c8f44d5ac8", Name = "Nike" }
                    },
                    new Product
                    {
                        Id = "99922149e773f2a3990b47fa",
                        Name = "Nike Mercurial Veer Football (White/Green/Black)",
                        Description = "Nike Mercurial Veer Football (White/Green/Black) Ball is made with colorful graphics that stand out on the field for easier ball tracking. A machine-stitched TPU casing delivers great touch and durability during play. ",
                        Price = 1450,
                        ImageFile = "images/products/Nike-Football-3.png",
                        Summary = "Nike Mercurial Veer Football (White/Green/Black)",
                        Types = new ProductType { Id = "63ca5d7d380402dce7f06ebc", Name = "Football" },
                        Brands = new ProductBrand { Id = "63ca5e7ec90ff5c8f44d5ac8", Name = "Nike" }
                    },
                    new Product
                    {
                        Id = "99932149e773f2a3990b47fa",
                        Name = "Babolat Team Line Racket 12 Kit Bag (Fluorescent Red)",
                        Description = "The Babolat Team Line racquet bag is highly durable and fashionable, holding up to 12 racquets.",
                        Price = 4550,
                        ImageFile = "images/products/babolat-kitback-1.png",
                        Summary = "Babolat Team Line Racket 12 Kit Bag (Fluorescent Red)",
                        Types = new ProductType { Id = "63ca5d8849bc19321b8be5f1", Name = "Kit Bags" },
                        Brands = new ProductBrand { Id = "63ca5e8d6110a9c56ee7dc48", Name = "Babolat" }
                    },
                    new Product
                    {
                        Id = "99942149e773f2a3990b47fa",
                        Name = "Babolat Pure Strike RH X12 Kit Bag (White/Red)",
                        Description = "Babolat Pure Strike 12-Pack Bag will effortlessly hold the majority of the rigging you should be fruitful on the court",
                        Price = 9799,
                        ImageFile = "images/products/babolat-kitback-2.png",
                        Summary = "Babolat Pure Strike RH X12 Kit Bag (White/Red)",
                        Types = new ProductType { Id = "63ca5d8849bc19321b8be5f1", Name = "Kit Bags" },
                        Brands = new ProductBrand { Id = "63ca5e8d6110a9c56ee7dc48", Name = "Babolat" }
                    },
                    new Product
                    {
                        Id = "99952149e773f2a3990b47fa",
                        Name = "Babolat Team Line 12 Racquet Kit Bag (Silver)",
                        Description = "Babolat Team Line 12 Racquet Kit Bag (Silver) for players who have tennis in their blood, Babolat brings you the Babolat Tennis Kit Bag India - Babolat Team Line Red 12 Pack.",
                        Price = 4550,
                        ImageFile = "images/products/babolat-kitback-3.png",
                        Summary = "Babolat Team Line 12 Racquet Kit Bag (Silver)",
                        Types = new ProductType { Id = "63ca5d8849bc19321b8be5f1", Name = "Kit Bags" },
                        Brands = new ProductBrand { Id = "63ca5e8d6110a9c56ee7dc48", Name = "Babolat" }
                    },
                    new Product
                    {
                        Id = "99962149e773f2a3990b47fa",
                        Name = "Yonex SUNR 4826TK BT6-SR Badminton Kit Bag (Black/Red/White)",
                        Description = "Yonex SUNR 4826TK BT6-SR Badminton Kit Bag (Black/Red/White)",
                        Price = 1999,
                        ImageFile = "images/products/yonex-kitback-1.png",
                        Summary = "Yonex SUNR 4826TK BT6-SR Badminton Kit Bag (Black/Red/White)",
                        Types = new ProductType { Id = "63ca5d8849bc19321b8be5f1", Name = "Kit Bags" },
                        Brands = new ProductBrand { Id = "63ca5e655ec1fdc49bd9327d", Name = "Yonex" }
                    },
                    new Product
                    {
                        Id = "99972149e773f2a3990b47fa",
                        Name = "Yonex SUNR LRB05 MS BT6 S Badminton Kit Bag (Blue/Red)",
                        Description = "Yonex SUNR LRB05 MS BT6 S Badminton Kit Bag (Blue/Red)",
                        Price = 1499,
                        ImageFile = "images/products/yonex-kitback-2.png",
                        Summary = "Yonex SUNR LRB05 MS BT6 S Badminton Kit Bag (Blue/Red)",
                        Types = new ProductType { Id = "63ca5d8849bc19321b8be5f1", Name = "Kit Bags" },
                        Brands = new ProductBrand { Id = "63ca5e655ec1fdc49bd9327d", Name = "Yonex" }
                    },
                    new Product
                    {
                        Id = "99982149e773f2a3990b47fa",
                        Name = "Yonex SUNR LRB05 MS BT6 S Badminton Kit Bag (Grey/Orange)",
                        Description = "Yonex SUNR LRB05 MS BT6 S Badminton Kit Bag (Grey/Orange)",
                        Price = 1499,
                        ImageFile = "images/products/yonex-kitback-3.png",
                        Summary = "Yonex SUNR LRB05 MS BT6 S Badminton Kit Bag (Grey/Orange)",
                        Types = new ProductType { Id = "63ca5d8849bc19321b8be5f1", Name = "Kit Bags" },
                        Brands = new ProductBrand { Id = "63ca5e655ec1fdc49bd9327d", Name = "Yonex" }
                    }
                };

                await productCollection.InsertManyAsync(products);
            }
        }

        ////public static async Task SeedDataAsync(IMongoCollection<Product> productCollection)
        ////{
        ////    if (!await productCollection.Find(b => true).AnyAsync())
        ////    {
        ////        var currentDir = Directory.GetCurrentDirectory();
        ////        Debug.WriteLine($"[ProductContextSeed] Current directory: {currentDir}");
        ////        Console.WriteLine($"[ProductContextSeed] Current directory: {currentDir}");
        ////        var path = Path.Combine(currentDir, "Data", "SeedData", "products.json");

        ////        // Log the path and file existence
        ////        Debug.WriteLine($"[ProductContextSeed] Looking for seed file at: {path}");
        ////        Console.WriteLine($"[ProductContextSeed] Looking for seed file at: {path}");

        ////        if (!File.Exists(path))
        ////        {
        ////            Debug.WriteLine($"[ProductContextSeed] File does not exist: {path}");
        ////            Console.WriteLine($"[ProductContextSeed] File does not exist: {path}");
        ////            throw new FileNotFoundException($"Seed data file not found at path: {path}");
        ////        }

        ////        var productData = File.ReadAllText(path);
        ////        var products = JsonSerializer.Deserialize<IEnumerable<Product>>(productData);

        ////        if (products is not null && products.Any())
        ////        {
        ////            await productCollection.InsertManyAsync(products);
        ////        }
        ////    }
        ////}
    }
}

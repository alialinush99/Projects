<?php

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;


class MovieSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table('movies')->insert([

            'name'=> 'Titanic',
            'genre' => 'Romance',
            'description' => "Titanic is a 1997 American epic romance and disaster film directed, written, co-produced, and co-edited by James Cameron. Incorporating both historical and fictionalized aspects, the film is based on accounts of the sinking of the RMS Titanic, and stars Leonardo DiCaprio and Kate Winslet as members of different social classes who fall in love aboard the ship during its ill-fated maiden voyage.",
            'cast' => 'Leonardo DiCaprio'	,
            'rating' => 9	,
            'release_year' => 1997
        ]);

        DB::table('movies')->insert([

            'name'=> 'The Wolf of a Wall Street',
            'genre' => 'Comedy',
            'cast' => 'Leonardo DiCaprio'	,
            'description' => "Based on the true story of Jordan Belfort, from his rise to a wealthy stock-broker living the high life to his fall involving crime, corruption and the federal government. ",
            'rating' => 7.5	,
            'release_year' => 2013


        ]);


        
        DB::table('movies')->insert([

            'name'=> 'The Matrix',
            'genre' => 'Action',
            'cast' => 'Keanu Reeves'	,
            'description' => "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
            'rating' => 8.7	,
            'release_year' => 1999


        ]);



        
        DB::table('movies')->insert([

            'name'=> 'Up',
            'genre' => 'Animation',
            'cast' => 'Edward Asner'	,
            'description' => "78-year-old Carl Fredricksen travels to Paradise Falls in his house equipped with balloons, inadvertently taking a young stowaway.",
            'rating' => 8.2	,
            'release_year' => 2009


        ]);




        
        DB::table('movies')->insert([

            'name'=> 'Joker',
            'genre' => 'Crima',
            'cast' => 'Joaquin Phoenix'	,
            'description' => "In Gotham City, mentally troubled comedian Arthur Fleck is disregarded and mistreated by society. He then embarks on a downward spiral of revolution and bloody crime. This path brings him face-to-face with his alter-ego: the Joker",
            'rating' => 8.4	,
            'release_year' => 2019


        ]);
    }
}

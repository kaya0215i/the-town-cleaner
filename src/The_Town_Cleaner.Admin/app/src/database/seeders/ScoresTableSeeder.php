<?php

namespace Database\Seeders;

use App\Models\Score;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class ScoresTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        Score::create([
            'value' => 100,
            'user_id' => 1,
            'stage_id' => 1,
        ]);
        Score::create([
            'value' => 200,
            'user_id' => 2,
            'stage_id' => 1,
        ]);
        Score::create([
            'value' => 300,
            'user_id' => 3,
            'stage_id' => 1,
        ]);
        Score::create([
            'value' => 400,
            'user_id' => 4,
            'stage_id' => 1,
        ]);
        Score::create([
            'value' => 500,
            'user_id' => 5,
            'stage_id' => 1,
        ]);

    }
}

<?php

namespace Database\Seeders;

use App\Models\Stage;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class StagesTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        Stage::create([
            'name' => 'Stage_1',
        ]);
        Stage::create([
            'name' => 'Stage_2',
        ]);
        Stage::create([
            'name' => 'Stage_3',
        ]);
    }
}

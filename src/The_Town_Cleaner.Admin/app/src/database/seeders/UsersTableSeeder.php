<?php

namespace Database\Seeders;

use App\Models\User;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class UsersTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        User::create([
            'name' => 'Player1001',
        ]);
        User::create([
            'name' => 'Player1002',
        ]);
        User::create([
            'name' => 'Player1003',
        ]);
        User::create([
            'name' => 'Player1004',
        ]);
        User::create([
            'name' => 'Player1005',
        ]);
    }
}

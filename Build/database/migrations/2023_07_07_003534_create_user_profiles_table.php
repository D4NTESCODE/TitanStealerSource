<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up(): void
    {
        Schema::create('user_profiles', function (Blueprint $table) {
            $table->id();
            $table->foreignId('hwid_id')->references('id')->on('hwids')->cascadeOnDelete();
            $table->string('guid')->nullable();
            $table->string('processor')->nullable();
            $table->string('graphic_card')->nullable();
            $table->string('ram')->nullable();
            $table->string('desktop_resolution')->nullable();
            $table->string('ip')->nullable();
            $table->string('country')->nullable();
            $table->string('execute_path')->nullable();
            $table->string('os')->nullable();
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('user_profiles');
    }
};

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
        Schema::create('cards', function (Blueprint $table) {
            $table->id();
            $table->foreignId('detected_browser_id')->references('id')->on('detected_browsers')->cascadeOnDelete();
            $table->string('name_in_card')->nullable();
            $table->string('expiration_month')->nullable();
            $table->string('expiration_year')->nullable();
            $table->string('card_number')->nullable();
            $table->string('billing_address_id')->nullable();
            $table->string('nickname')->nullable();
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('cards');
    }
};

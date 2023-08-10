<?php

namespace App\Models;

use App\Models\Relations\BelongsToDetectedBrowser;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Card extends Model
{
    use HasFactory;
    use BelongsToDetectedBrowser;

    protected $fillable = [
        'detected_browser_id',
        'name_in_card',
        'expiration_month',
        'expiration_year',
        'card_number',
        'billing_address_id',
        'nickname'
    ];
}

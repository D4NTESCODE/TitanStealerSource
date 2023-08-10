<?php

namespace App\Models;

use App\Models\Relations\BelongsToDetectedBrowser;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Autofill extends Model
{
    use HasFactory;
    use BelongsToDetectedBrowser;

    protected $fillable = [
        'detected_browser_id',
        'name',
        'value'
    ];
}

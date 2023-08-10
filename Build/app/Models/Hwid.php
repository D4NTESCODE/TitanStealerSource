<?php

namespace App\Models;

use App\Models\Relations\HasDetectedBrowsers;
use App\Models\Relations\HasUserProfile;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Hwid extends Model
{
    use HasFactory;
    use HasDetectedBrowsers;
    use HasUserProfile;

    protected $fillable = [
        'id',
        'hash'
    ];
}

<?php

namespace App\Models;

use App\Models\Relations\BelongsToHwid;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class UserProfile extends Model
{
    use HasFactory;
    use BelongsToHwid;

    protected $fillable = [
        'hwid_id',
        'guid',
        'processor',
        'graphic_card',
        'ram',
        'desktop_resolution',
        'ip',
        'country',
        'execute_path',
        'os'
    ];
}

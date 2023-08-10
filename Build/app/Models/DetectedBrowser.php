<?php

namespace App\Models;

use App\Models\Relations\BelongsToHwid;
use App\Models\Relations\HasAutofills;
use App\Models\Relations\HasCards;
use App\Models\Relations\HasCookies;
use App\Models\Relations\HasLoginRecords;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class DetectedBrowser extends Model
{
    use HasFactory;
    use BelongsToHwid;
    use HasAutofills;
    use HasCards;
    use HasCookies;
    use HasLoginRecords;

    protected $fillable = [
        'hwid_id',
        'name'
    ];
}

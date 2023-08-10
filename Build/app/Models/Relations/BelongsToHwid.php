<?php

namespace App\Models\Relations;

use App\Models\Hwid;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

trait BelongsToHwid {
    public function hwid(): BelongsTo {
        return $this->belongsTo(Hwid::class);
    }
}

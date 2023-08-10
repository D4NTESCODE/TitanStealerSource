<?php

namespace App\Models\Relations;

use App\Models\LoginRecord;
use Illuminate\Database\Eloquent\Relations\HasMany;

trait HasLoginRecords {
    public function loginRecords(): HasMany {
        return $this->hasMany(LoginRecord::class);
    }
}

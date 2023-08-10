<?php

namespace App\Models\Relations;

use App\Models\Cookie;
use Illuminate\Database\Eloquent\Relations\HasMany;

trait HasCookies {
    public function cookies(): HasMany {
        return $this->hasMany(Cookie::class);
    }
}

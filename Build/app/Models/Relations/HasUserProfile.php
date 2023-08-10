<?php

namespace App\Models\Relations;

use App\Models\UserProfile;
use Illuminate\Database\Eloquent\Relations\HasOne;

trait HasUserProfile {
    public function profile(): HasOne {
        return $this->hasOne(UserProfile::class);
    }
}

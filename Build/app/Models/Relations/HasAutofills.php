<?php

namespace App\Models\Relations;

use App\Models\Autofill;
use Illuminate\Database\Eloquent\Relations\HasMany;

trait HasAutofills {
    public function autofills(): HasMany {
        return $this->hasMany(Autofill::class);
    }
}

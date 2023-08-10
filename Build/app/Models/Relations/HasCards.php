<?php

namespace App\Models\Relations;

use App\Models\Card;
use Illuminate\Database\Eloquent\Relations\HasMany;

trait HasCards {
    public function cards(): HasMany {
        return $this->hasMany(Card::class);
    }
}

<?php

namespace App\Models\Relations;

use App\Models\DetectedBrowser;
use Illuminate\Database\Eloquent\Relations\HasMany;

trait HasDetectedBrowsers {
    public function browsers(): HasMany {
        return $this->hasMany(DetectedBrowser::class);
    }
}

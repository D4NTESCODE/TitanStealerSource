<?php

namespace App\Models\Relations;

use App\Models\DetectedBrowser;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

trait BelongsToDetectedBrowser {
    public function browser(): BelongsTo {
        return $this->belongsTo(DetectedBrowser::class, 'detected_browser_id', 'id');
    }
}

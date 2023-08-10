<?php

namespace App\Filament\Resources\HwidResource\Pages;

use App\Filament\Resources\HwidResource;
use Filament\Pages\Actions;
use Filament\Resources\Pages\EditRecord;

class EditHwid extends EditRecord
{
    protected static string $resource = HwidResource::class;

    protected function getActions(): array
    {
        return [
            Actions\DeleteAction::make(),
        ];
    }
}

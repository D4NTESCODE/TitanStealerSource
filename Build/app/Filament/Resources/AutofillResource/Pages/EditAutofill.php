<?php

namespace App\Filament\Resources\AutofillResource\Pages;

use App\Filament\Resources\AutofillResource;
use Filament\Pages\Actions;
use Filament\Resources\Pages\EditRecord;

class EditAutofill extends EditRecord
{
    protected static string $resource = AutofillResource::class;

    protected function getActions(): array
    {
        return [
            Actions\DeleteAction::make(),
        ];
    }
}

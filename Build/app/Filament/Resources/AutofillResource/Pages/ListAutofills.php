<?php

namespace App\Filament\Resources\AutofillResource\Pages;

use App\Filament\Resources\AutofillResource;
use Filament\Pages\Actions;
use Filament\Resources\Pages\ListRecords;

class ListAutofills extends ListRecords
{
    protected static string $resource = AutofillResource::class;

    protected function getActions(): array
    {
        return [
           // Actions\CreateAction::make(),
        ];
    }
}

<?php

namespace App\Filament\Resources;

use App\Filament\Resources\AutofillResource\Pages;
use App\Filament\Resources\AutofillResource\RelationManagers;
use App\Models\Autofill;
use Filament\Forms;
use Filament\Resources\Form;
use Filament\Resources\Resource;
use Filament\Resources\Table;
use Filament\Tables;
use Illuminate\Database\Eloquent\Builder;
use Illuminate\Database\Eloquent\SoftDeletingScope;

class AutofillResource extends Resource
{
    protected static ?string $model = Autofill::class;
    protected static ?string $navigationGroup = 'Logs';
    protected static ?string $navigationIcon = 'heroicon-o-tag';

    public static function form(Form $form): Form
    {
        return $form
            ->schema([

            ]);
    }

    public static function table(Table $table): Table
    {
        return $table
            ->columns([
                Tables\Columns\TextColumn::make('browser.hwid.hash'),
                Tables\Columns\TextColumn::make('browser.name'),
                Tables\Columns\TextColumn::make('name'),
                Tables\Columns\TextColumn::make('value'),
            ])
            ->filters([
                //
            ])
            ->actions([
                //Tables\Actions\EditAction::make(),
            ])
            ->bulkActions([
                Tables\Actions\DeleteBulkAction::make(),
            ]);
    }

    public static function getRelations(): array
    {
        return [
            //
        ];
    }

    public static function getPages(): array
    {
        return [
            'index' => Pages\ListAutofills::route('/'),
            'create' => Pages\CreateAutofill::route('/create'),
            'edit' => Pages\EditAutofill::route('/{record}/edit'),
        ];
    }
}

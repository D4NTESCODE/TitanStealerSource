<?php

namespace App\Filament\Resources;

use App\Filament\Resources\UserProfileResource\Pages;
use App\Filament\Resources\UserProfileResource\RelationManagers;
use App\Models\UserProfile;
use Filament\Forms;
use Filament\Resources\Form;
use Filament\Resources\Resource;
use Filament\Resources\Table;
use Filament\Tables;
use Illuminate\Database\Eloquent\Builder;
use Illuminate\Database\Eloquent\SoftDeletingScope;

class UserProfileResource extends Resource
{
    protected static ?string $model = UserProfile::class;
    protected static ?string $navigationGroup = 'General';
    protected static ?string $navigationIcon = 'heroicon-o-user';

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
                Tables\Columns\TextColumn::make('guid'),
                Tables\Columns\TextColumn::make('processor'),
                Tables\Columns\TextColumn::make('graphic_card'),
                Tables\Columns\TextColumn::make('ram'),
                Tables\Columns\TextColumn::make('desktop_resolution'),
                Tables\Columns\TextColumn::make('ip'),
                Tables\Columns\TextColumn::make('country'),
                Tables\Columns\TextColumn::make('execute_path'),
                Tables\Columns\TextColumn::make('os'),
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
            'index' => Pages\ListUserProfiles::route('/'),
            'create' => Pages\CreateUserProfile::route('/create'),
            'edit' => Pages\EditUserProfile::route('/{record}/edit'),
        ];
    }
}

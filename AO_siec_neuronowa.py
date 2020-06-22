#Siec neuronowa do rozpoznawania plci

import tensorflow as tf
print(tf.__version__)
import numpy as np
import matplotlib.pyplot as plt
import os
import tensorflow as tf

from tensorflow.python.keras.preprocessing.image import img_to_array
from tensorflow.python.keras.models import Sequential
from tensorflow.python.keras.layers import Conv2D
from tensorflow.python.keras.layers import MaxPooling2D
from tensorflow.python.keras.layers import Flatten
from tensorflow.python.keras.layers import Dense
from tensorflow.python.keras.layers import Dropout

os.environ['CUDA_VISIBLE_DEVICES'] = '-1'

Base_path = os.path.dirname(os.path.abspath(__file__))
Train_path = os.path.join(Base_path, r'Baza\\training')
TrainM_path = os.path.join(Base_path, r'Baza\\training\\M')
TrainK_path = os.path.join(Base_path, r'Baza\\training\\K')
Test_path = os.path.join(Base_path, r'Baza\\Test')
TestM_path = os.path.join(Base_path, r'Baza\\Test\\M')
TestK_path = os.path.join(Base_path, r'Baza\\Test\\K')

size_test_M = len(os.listdir(TestM_path))
size_test_K = len(os.listdir(TestK_path))
size_test = size_test_M + size_test_K
size_K = len(os.listdir(TrainK_path))
size_M = len(os.listdir(TrainM_path))
size_train = size_K+size_M

def preprocess_image(image, target_size):
    image = image.resize(target_size)
    image = img_to_array(target_size)
    image = np.expand_dims(image, axis=0)
    return image


if __name__ == '__main__':
    BATCH_SIZE = 250
    EPOCHS = 7
    IMG_HEIGHT = 180
    IMG_WIDTH = 180

    train_image_generator = tf.keras.preprocessing.image.ImageDataGenerator(rescale=1. / 255)
    test_image_generator = tf.keras.preprocessing.image.ImageDataGenerator(rescale=1. / 255)

    train_data_gen = train_image_generator.flow_from_directory(directory=str(Train_path),
                                                         batch_size=BATCH_SIZE,
                                                         shuffle=True,
                                                         target_size=(IMG_HEIGHT, IMG_WIDTH),
                                                         class_mode='binary')

    test_data_gen = train_image_generator.flow_from_directory(directory=str(Test_path),
                                                               batch_size=BATCH_SIZE,
                                                               shuffle=True,
                                                               target_size=(IMG_HEIGHT, IMG_WIDTH),
                                                               class_mode='binary')

    classifier = Sequential()

    classifier.add(Conv2D(16, (3, 3), input_shape=(180, 180, 3),
                          activation='relu', padding='same'))
    classifier.add(MaxPooling2D())
    classifier.add(Dropout(0.2))
    classifier.add(Conv2D(32, (3, 3), activation='relu', padding='same'))
    classifier.add(MaxPooling2D())
    classifier.add(Dropout(0.2))
    classifier.add(Conv2D(64, (3, 3), activation='relu', padding='same'))
    classifier.add(MaxPooling2D())
    classifier.add(Flatten())
    classifier.add(Dense(512, activation='relu'))
    classifier.add(Dense(1, activation='sigmoid'))

    classifier.compile(optimizer='adam', loss='binary_crossentropy', metrics=["accuracy"])

    classifier.summary()

    history = classifier.fit_generator(
        train_data_gen,
        steps_per_epoch=size_train // BATCH_SIZE,
        epochs=EPOCHS,
        validation_data=test_data_gen,
        validation_steps=size_test // BATCH_SIZE
    )

acc = history.history['accuracy']
val_acc = history.history['val_accuracy']

loss = history.history['loss']
val_loss = history.history['val_loss']

epochs_range = range(EPOCHS)

plt.figure(figsize=(8, 8))
plt.subplot(1, 2, 1)
plt.plot(epochs_range, acc, label='Skuteczność treningowa')
plt.plot(epochs_range, val_acc, label='Skuteczność testowa')
plt.legend(loc='lower right')
plt.title('Skuteczność treningowa i testowa')

plt.subplot(1, 2, 2)
plt.plot(epochs_range, loss, label='Straty podczas treningu')
plt.plot(epochs_range, val_loss, label='Straty podczas testu')
plt.legend(loc='upper right')
plt.title('Straty podczas treningu i testu')
plt.show()

savepath1 = os.path.join(Base_path, r'MvsK_model.h5')
savepath2 = os.path.join(Base_path, r'MvsK_model_weights.h5')
tf.keras.models.save_model(
    classifier, savepath1
)
classifier.save_weights(savepath2)

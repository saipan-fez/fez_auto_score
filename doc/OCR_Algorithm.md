# OCR Algorithm

## �͂��߂�

FEZ�̃X�R�A��ʂ╶���T�C�Y���𑜓x�Ɉˑ������A��Ɉ��̑傫���ł��邱�Ƃ𗘗p���āA�s�N�Z���̐F�̃p�^�[�����當��(����)�𔻕ʂ�����@���̗p���Ă��܂��B  
[���v�X�R�A�����E�ڍ׃X�R�A�����E�L�����Ȃǂ̕���](./score_sample.png)�̌v3�����Ŏg�p����Ă���t�H���g���قȂ邽�߁A���ꂼ��̕������Ƃɐ�p�̃A���S���Y����p���Ă��܂��B  
�܂��A�}�b�v���ƃX�L���������͎�肤��p�^�[�������Œ�̂��߁A���O�ɂ��̕����̉摜��p�ӂ��A���̉摜�Ɗ��S��v������̂��������Ă��܂��B  

> ������OCR�Z�p�̗��p�ɂ���  
> OCR���C�u����(Tesseract��)�F��F�����������������ߕs�̗p  
> �N���E�h�T�[�r�X(GoogleCloudVision��)�F�����ʓ�����s�̗p


## �ڍ׃X�R�A�����̃A���S���Y��

�摜��y=0�̗�𑖍����A#FFFFFF�̃s�N�Z�����܂�������i�ȍ~���̃s�N�Z�����u�擪�s�N�Z���v�Ƃ���j�B  
��������E�����ɉ��s�N�Z��#FFFFF���A�����Ă��邩��3�̃O���[�v(1, 2, 4px)�ɕ��ނ���B  
����ɂ�������A�e�O���[�v���ɈقȂ�A���S���Y���ŕ��ނ���B  

* 1px
	* �擪�s�N�Z������(-1, +2)px�̐F��#FFFFFF���ǂ���
		* true :4
		* false:1
* 2px
	* �擪�s�N�Z������(-1, +3), (-1, +4), (-1, +5), (0, +4)px�̐F�����ꂼ��#FFFFFF���ǂ���
		* true,  true,  true,  true :6
		* true,  true,  true,  false:0
		* true,  false, true,  true :8
		* true,  false, false, true :9
		* false, false, false, false:2
		* false, false, false, true :3
* 4px
	* �擪�s�N�Z������(0, +1)px�̐F��#FFFFFF���ǂ���
		* true :5
		* false:7

�`�F�b�N����s�N�Z���ꗗ(�ԐF����)  
<img src=./DetailScore.png />


## ���v�X�R�A�����̃A���S���Y��

�摜��y=0�̗�𑖍����A#FFFFFF�̃s�N�Z�����܂�������i�ȍ~���̃s�N�Z�����u�擪�s�N�Z���v�Ƃ���j�B  
��������E�����ɉ��s�N�Z��#FFFFF���A�����Ă��邩��3�̃O���[�v(1, 3, 5px)�ɕ��ނ���B  
����ɂ�������A�e�O���[�v���ɈقȂ�A���S���Y���ŕ��ނ���B  

* 1px
	* �擪�s�N�Z������(-1, +2)px�̐F��#FFFFFF���ǂ���
		* true :4
		* false:1
* 3px
	* �擪�s�N�Z������(-1, +4), (-1, +5), (-1, +6), (0, +5)px�̐F�����ꂼ��#FFFFFF���ǂ���
		* true,  true,  true,  true :6
		* true,  true,  true,  false:0
		* true,  false, true,  true :8
		* true,  true,  false, false:9
		* false, false, false, false:2
		* false, false, false, true :3
* 5px
	* �擪�s�N�Z������(0, +1)px�̐F��#FFFFFF���ǂ���
		* true :5
		* false:7

�`�F�b�N����s�N�Z���ꗗ(�ԐF����)  
<img src=./TotalScore.png />


## �L���������̃A���S���Y��

�܂��摜���l��(#FFFFFF�ȊO��#000000��)����B
�摜��y=5�̗�𑖍����A#FFFFFF�̃s�N�Z�����܂�������i�ȍ~���̃s�N�Z�����u�擪�s�N�Z���v�Ƃ���j�B  
��������E�����ɉ��s�N�Z��#FFFFF���A�����Ă��邩��2�̃O���[�v(1, 2, 3px)�ɕ��ނ���B  
����ɂ�������A�e�O���[�v���ɈقȂ�A���S���Y���ŕ��ނ���B

* 1px
    * �擪�s�N�Z������(+1, -1), (0. +1), (+1, +1), (+1, +2)px�̐F�����ꂼ��#FFFFFF���ǂ���
        * true , true , false, true :0
        * false, true , true , false:9
        * false, true , false, false:1
        * false, false, true , false:3
        * false, false, false, false:4
        * true , true , false, false
            * �擪�s�N�Z������(+1, -2)px�̐F��#FFFFFF���ǂ���
                * true :7
                * false:2
* 2px
    * �擪�s�N�Z������(0, +3)px�̐F��#FFFFFF���ǂ���
        * true :8
        * false:5
* 3px
    * 6
    
��l����̉摜�ƃ`�F�b�N����s�N�Z���ꗗ(�ԐF����)  
<img src=./KillScore.png />

## �}�b�v�������̃A���S���Y��

���O�Ɏ蓮�Ŋe�}�b�v�̉摜���B�e����B�����āA�}�b�v���\��������؂�o�������0x000000�ȊO�̃s�N�Z����0xFFFFFF�ɕϊ������摜��p�ӂ���(Resource/MapNameBitmap���̉摜)�B  
���̉摜��MD5�ƁA��L�Ɠ����؂�o�����ϊ����s�����Ώۂ̉摜��MD5���r���āA���S��v������̂���������B  
�Ȃ��A�}�b�v���̉摜�B�e���ʓ|�Ȃ��߁A�����嗤�ȊO�͑ΏۊO(�s���ƕ\��)�Ƃ��Ă���B  

## �X�L���������̃A���S���Y��
���O�Ɏ蓮�Ŋe�X�L���̉摜���B�e����B�����āA�X�L�����\��������؂�o�����摜��p�ӂ���(Resource/SkillBitmap���̉摜)�B  
���̉摜��MD5�ƁA��L�Ɠ����؂�o�����ϊ����s�����Ώۂ̉摜��MD5���r���āA���S��v������̂���������B  
�Ȃ��A�Z�X�^�X�̃Q�C�U�[�̗��ߒ��Ƃ������X�L���\�����������Ԍo�߂Ő؂�ւ����͖̂ʓ|�Ȃ��ߑΏۊO(�s���ƕ\��)�Ƃ��Ă���B
